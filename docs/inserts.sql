

/*Usuarios*/
/*
Email													Contrase�a					Rol
admin1@example.com						contrasena1					admin
brunolopez25702blr@gmail.com	contrasena2					repartidor
usuario1@example.com					contrasena3					cliente
*/
/*Distribuidores*/
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('SullivanBurger', 'Avenida Virgen del Roc�o 3', '689325436');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Frutas Javier Cuevas', 'Labrador, 47. Pol�gono Pagusa', '901234567');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Lanjar�n', 'Calle de las Bebidas 321', '728171954');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('CocaCola', 'Calle de las Bebidas 321', '678901234');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Fanta', 'Calle de las Bebidas 321', '632987234');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Cervecer�a Paco', 'Calle de las Bebidas 321', '678308214');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Carreta Distribuciones', 'Pol. Ind. Guadalquivir, C. de la Tecnolog�a, 17', '636069717');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('La Ruta de Kaldi', 'Jos� Moreno Carbonero, Edif Recaredo- local', '617490617');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Manu Jara', 'Calle Pureza, 5', '695979892');
INSERT INTO Distribuidores (nombre, direccion, telefono) VALUES ('Dolce Mar�a', 'Poli. Ind Pi�ero, Carretera del Puerto, Nave 3', '772004049');


/*
Productos
Hamburguesas
*/

INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Awebo', 'Pulled pork con bacon, queso, aros de cebolla, lechuga y guacamole.', 7.99, 'awebo.png', 60, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Sullivan', 'La estrella de la casa, ternera madurada, ingredientes secretos.', 7.00, 'sullivan.png', 30, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Esencial', 'La simpleza con ternera, bacon, queso y ketchup.', 4.99, 'esencial.png', 100, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Pecado Carnal', 'Utilizando ternera y pulled pork, hamburguesa para carn�voros.', 7.50, 'pecado_carnal.png', 60, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Texas', 'Ternera con lechuga, bacon, queso, cebolla crujiente y salsa barbacoa.', 5.99, 'texas.png', 20, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Ib�rica', 'Black Angus con jam�n ib�rico, queso viejo y salmorejo en pan artesano.', 12.50, 'iberica.png', 50, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Vegana', 'Hamburguesa 100% vegetal con guacamole, cebolla, tomate y lechuga en pan de cereales.', 9.00, 'vegana.png', 20, 'hamburguesa', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Crispy chicken', 'Hamburguesa crujiente de pollo en pan artesano con bacon, lechuga y salsa alioli.', 5.50, 'crispy_chicken.png', 50, 'hamburguesa', 'SullivanBurger');

/*
Productos
Complementos
*/
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Patatas Fritas', 'Crujientes patatas fritas doradas y saladas', 1.49, 'patatas_fritas.png', 100, 'complemento', 'Frutas Javier Cuevas');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Ensalada C�sar', 'Sabrosa ensalada C�sar con pollo a la parrilla', 4.99, 'ensalada_cesar.png', 15, 'complemento', 'Frutas Javier Cuevas');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Ensalada Caprese', 'Fresca ensalada Caprese con tomate, mozzarella y albahaca', 5.99, 'ensalada_caprese.png', 15, 'complemento', 'Frutas Javier Cuevas');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Nachos con Queso', 'Crunchy nachos con salsa de queso derretido', 4.99, 'nachos_queso.png', 35, 'complemento', 'SullivanBurger');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Alitas de pollo', '6 unidades de alitas de pollo ahumadas', 1.75, 'alitas_pollo.png', 50, 'complemento', 'SullivanBurger');

/*
Productos
Bebidas
*/
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('CocaCola', 'Refrescante bebida de cola', 1.99, 'cocacola.png', 200, 'bebida', 'CocaCola');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('CocaCola Zero', 'Refrescante bebida de cola sin az�cares a�adidos', 1.99, 'cocacola_zero.png', 100, 'bebida', 'CocaCola');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Fanta naranja', 'Refrescante bebida de naranja', 1.99, 'fanta_naranja.png', 100, 'bebida', 'Fanta');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Fanta lim�n', 'Refrescante bebida de naranja lim�n', 1.99, 'fanta_limon.png', 100, 'bebida', 'Fanta');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId)
VALUES ('Batido de fresa', 'Refrescante batido de fresa con trozos de fruta', 1.20, 'batido_fresa.png', 50, 'bebida', 'Carreta Distribuciones');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId)
VALUES ('Batido de chocolate', 'Refrescante batido con sabor chocolate', 1.20, 'batido_chocolate.png', 50, 'bebida', 'Carreta Distribuciones');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId)
VALUES ('Cerveza', 'Cerveza malteada con centeno', 2.25, 'cerveza.png', 200, 'bebida', 'Cervecer�a Paco');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId)
VALUES ('Caf� Americano', 'Cl�sico caf� americano reci�n hecho', 2.49, 'cafe_americano.png', 50, 'bebida', 'La Ruta de Kaldi');

/*
Productos
Postres
*/
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Tarta de chocolate', 'Irresistible tarta de chocolate con ganache.', 6.99, 'tarta_chocolate.png', 20, 'postre', 'Manu Jara');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Brownie de chocolate', 'Esponjoso brownie de chocolate con nueces.', 5.99, 'brownie_chocolate.png', 30, 'postre', 'Manu Jara');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Galletas de avena', 'Deliciosas galletas de avena con chips de chocolate.', 3.99, 'galletas_avena.png', 40, 'postre', 'Manu Jara');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Tarta de manzana', 'Deliciosa tarta de manzana casera.', 6.99, 'tarta_manzana.jpg', 18, 'postre', 'Manu Jara');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Helado de vainilla', 'Delicioso helado de vainilla con trozos de caramelo.', 4.99, 'helado_vainilla.png', 30, 'postre', 'Dolce Mar�a');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Helado de fresa', 'Delicioso helado de fresa con cobertura de chocolate.', 4.99, 'helado_fresa.png', 30, 'postre', 'Dolce Mar�a');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Helado de chocolate', 'Delicioso helado de chocolate puro.', 4.99, 'helado_chocolate.png', 30, 'postre', 'Dolce Mar�a');
INSERT INTO Productos (Nombre, Descripcion, Precio, Imagen, Stock, Tipo, DistribuidorId) 
VALUES ('Helado de nata', 'Delicioso helado de nata con sirope de fresa.', 4.99, 'helado_nata.png', 30, 'postre', 'Dolce Mar�a');




