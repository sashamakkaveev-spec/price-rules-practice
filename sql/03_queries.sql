-- 1. Активные товары стоимостью не больше 200
SELECT name, price
FROM products
WHERE price <= 200 AND is_active = TRUE
ORDER BY price;


-- 2. История изменений цен активных товаров
SELECT p.name, h.old_price, h.new_price
FROM price_history AS h
         INNER JOIN products AS p ON p.id = h.product_id
WHERE p.is_active = TRUE
ORDER BY p.name, h.id;


-- 3. Количество изменений цены у каждого активного товара
SELECT p.name, COUNT(h.id) AS changes_count
FROM products AS p
         LEFT JOIN price_history AS h ON p.id = h.product_id
WHERE p.is_active = TRUE
GROUP BY p.id, p.name
ORDER BY changes_count DESC, p.name;


-- 4. Активные товары без истории изменений цены
SELECT p.name, p.price
FROM products AS p
         LEFT JOIN price_history AS h ON p.id = h.product_id
WHERE p.is_active = TRUE AND h.id IS NULL
ORDER BY p.name;


-- 5. Та же выборка через группировку и HAVING
SELECT p.name, COUNT(h.id) AS changes_count
FROM products AS p
         LEFT JOIN price_history AS h ON p.id = h.product_id
WHERE p.is_active = TRUE
GROUP BY p.id, p.name
HAVING COUNT(h.id) = 0
ORDER BY p.name;