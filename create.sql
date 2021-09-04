drop database pizzeria;
create database if not exists pizzeria;
use pizzeria;
#- о меню (название блюда, вес, фото, цена, описание, категория, доступность);
create table menu
(
id int primary key,
title varchar(225),
weigth float,
photo varchar(225),
coast int,
category varchar(225),
is_enable bool
);
#- о заказе (номер заказа, дата, №столика, ФИО официанта);
create table order_table
(
id int primary key,
date_of_order date,
num_of_place int,
fio varchar(225)
);
#- о содержании заказа (заказ, блюдо, количество).
create table order_list
(
id int primary key,
id_order int,
id_dish int,
amount int,
foreign key (id_order) references order_table(id) on delete cascade on update cascade,
foreign key (id_dish) references menu(id) on delete cascade on update cascade
);