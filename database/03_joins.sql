-- INNER

SELECT
    u.name,
    t.title,
    t.status
FROM users AS u
INNER JOIN kanban_tasks AS t
    ON u.id = t.assignee_id;



-- LEFT

SELECT
    u.name,
    t.title,
    t.status
FROM users AS u
LEFT JOIN kanban_tasks AS t
    ON u.id = t.assignee_id;



-- RIGHT

SELECT
    u.name,
    t.title,
    t.status
FROM users AS u
RIGHT JOIN kanban_tasks AS t
    ON u.id = t.assignee_id;



-- FULL OUTER

SELECT
    u.name,
    t.title,
    t.status
FROM users AS u
FULL OUTER JOIN kanban_tasks AS t
    ON u.id = t.assignee_id;