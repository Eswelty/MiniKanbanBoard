import { Component, inject, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import {
  ApiService, User, UserData,
  KanbanTask, TaskData, Status
} from './api.service';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private api = inject(ApiService);

  users = signal<User[]>([]);
  tasks = signal<KanbanTask[]>([]);
  error = signal('');
  busy = signal(false);

  statuses: Status[] = ['To Do', 'In Progress', 'Done'];

  filterId: number | null = null;

  editingTaskId: number | null = null;
  editingUserId: number | null = null;

  taskForm: TaskData = {
    title: '',
    description: '',
    status: 'To Do',
    assigneeId: null
  };

  userForm: UserData = {
    name: '',
    email: ''
  };

  ngOnInit() {
    this.run(() => this.load());
  }

  private async run(action: () => Promise<void>) {
    if (this.busy()) return;

    this.busy.set(true);
    this.error.set('');

    try {
      await action();
    } catch (e: any) {
      console.error(e);
      this.error.set(
        typeof e.error === 'string'
          ? e.error
          : e.error?.title || e.message || 'Ошибка запроса'
      );
    } finally {
      this.busy.set(false);
    }
  }

  private async load() {
    const [users, tasks] = await Promise.all([
      firstValueFrom(this.api.getUsers()),
      firstValueFrom(this.api.getTasks(this.filterId))
    ]);

    this.users.set(users);
    this.tasks.set(tasks);
  }

  changeFilter(id: number | null) {
    this.filterId = id;
    this.run(() => this.load());
  }

  tasksByStatus(status: Status) {
    return this.tasks().filter(t => t.status === status);
  }

  assigneeName(id: number | null) {
    return this.users().find(u => u.id === id)?.name
      ?? 'Не назначен';
  }

  saveTask() {
    this.run(async () => {
      if (this.editingTaskId === null) {
        await firstValueFrom(
          this.api.createTask(this.taskForm)
        );
      } else {
        await firstValueFrom(
          this.api.updateTask(
            this.editingTaskId, this.taskForm
          )
        );
      }

      this.resetTask();
      await this.load();
    });
  }

  editTask(task: KanbanTask) {
    this.editingTaskId = task.id;

    this.taskForm = {
      title: task.title,
      description: task.description,
      status: task.status,
      assigneeId: task.assigneeId
    };
  }

  resetTask() {
    this.editingTaskId = null;

    this.taskForm = {
      title: '',
      description: '',
      status: 'To Do',
      assigneeId: null
    };
  }

  changeStatus(task: KanbanTask, status: Status) {
    this.run(async () => {
      await firstValueFrom(
        this.api.updateTask(task.id, {
          title: task.title,
          description: task.description,
          status: status,
          assigneeId: task.assigneeId
        })
      );

      await this.load();
    });
  }

  deleteTask(id: number) {
    if (!confirm('Удалить задачу?')) return;

    this.run(async () => {
      await firstValueFrom(this.api.deleteTask(id));

      if (this.editingTaskId === id)
        this.resetTask();

      await this.load();
    });
  }

  saveUser() {
    this.run(async () => {
      if (this.editingUserId === null) {
        await firstValueFrom(
          this.api.createUser(this.userForm)
        );
      } else {
        await firstValueFrom(
          this.api.updateUser(
            this.editingUserId, this.userForm
          )
        );
      }

      this.resetUser();
      await this.load();
    });
  }

  editUser(user: User) {
    this.editingUserId = user.id;

    this.userForm = {
      name: user.name,
      email: user.email
    };
  }

  resetUser() {
    this.editingUserId = null;
    this.userForm = { name: '', email: '' };
  }

  deleteUser(id: number) {
    if (!confirm('Удалить пользователя?')) return;

    this.run(async () => {
      await firstValueFrom(this.api.deleteUser(id));

      if (this.filterId === id)
        this.filterId = null;

      if (this.editingUserId === id)
        this.resetUser();

      if (this.taskForm.assigneeId === id)
        this.taskForm.assigneeId = null;

      await this.load();
    });
  }
}
