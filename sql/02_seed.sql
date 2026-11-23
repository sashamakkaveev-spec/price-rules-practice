-- Товары и их текущие цены
INSERT INTO products (id, name, price, is_active)
VALUES
    (1, 'Молоко', 110.00, TRUE),
    (2, 'Хлеб', 60.00, TRUE),
    (3, 'Сыр', 350.00, TRUE),
    (4, 'Йогурт', 90.00, TRUE),
    (5, 'Яблоки', 150.00, TRUE);

-- История изменений цен
INSERT INTO price_history (id, product_id, old_price, new_price)
VALUES
    (1, 2, 55.00, 60.00),
    (2, 4, 80.00, 90.00),
    (3, 1, 95.00, 110.00);