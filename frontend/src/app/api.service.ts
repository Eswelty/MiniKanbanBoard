import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export type Status = 'To Do' | 'In Progress' | 'Done';

export interface UserData {
  name: string;
  email: string;
}

export interface User extends UserData {
  id: number;
}

export interface TaskData {
  title: string;
  description: string | null;
  status: Status;
  assigneeId: number | null;
}

export interface KanbanTask extends TaskData {
  id: number;
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private url = 'https://localhost:7222/api';

  getUsers() {
    return this.http.get<User[]>(`${this.url}/users`);
  }

  createUser(user: UserData) {
    return this.http.post<User>(`${this.url}/users`, user);
  }

  updateUser(id: number, user: UserData) {
    return this.http.put<User>(`${this.url}/users/${id}`, user);
  }

  deleteUser(id: number) {
    return this.http.delete<void>(`${this.url}/users/${id}`);
  }

  getTasks(assigneeId: number | null = null) {
    const url = assigneeId === null
      ? `${this.url}/tasks`
      : `${this.url}/tasks?assigneeId=${assigneeId}`;

    return this.http.get<KanbanTask[]>(url);
  }

  createTask(task: TaskData) {
    return this.http.post<KanbanTask>(`${this.url}/tasks`, task);
  }

  updateTask(id: number, task: TaskData) {
    return this.http.put<KanbanTask>(
      `${this.url}/tasks/${id}`, task
    );
  }

  deleteTask(id: number) {
    return this.http.delete<void>(`${this.url}/tasks/${id}`);
  }
}
