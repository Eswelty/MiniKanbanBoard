-- users

INSERT INTO users (name, email)
VALUES
    ('Анна', 'anna@ex.com'),
    ('Иван', 'ivan@ex.com'),
    ('Мария', 'maria@ex.com');


-- tasks

INSERT INTO kanban_tasks
    (title, description, status, assignee_id)
VALUES
    (
        'Изучить Angular',
        'Разобраться с компонентами',
        'To Do',
        (SELECT id FROM users WHERE email = 'anna@ex.com')
    ),
    (
        'Разработать API',
        'Создать контроллеры',
        'In Progress',
        (SELECT id FROM users WHERE email = 'ivan@ex.com')
    ),
    (
        'Подготовить документацию',
        'Написать README',
        'Done',
        NULL
    );