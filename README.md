# Sistema Gestor de Ventas e Inventario

## Estudiante

**Nombre completo:** Nicolas David Castillo Rojano

---

## Descripción del proyecto

El **Sistema Gestor de Ventas e Inventario** es una aplicación desarrollada en **C#**, cuyo objetivo es facilitar la gestión de productos, inventario y ventas de un pequeño negocio.

El sistema permite registrar productos, consultar el inventario, controlar el stock, realizar ventas y generar información relacionada con las operaciones realizadas.

### Funcionalidades principales

- Registrar nuevos productos en el inventario.
- Consultar el inventario completo.
- Visualizar el precio y stock de cada producto.
- Mostrar alertas cuando un producto tiene bajo stock.
- Registrar ventas de productos.
- Actualizar automáticamente el stock después de una venta.
- Aplicar un descuento del 10% para clientes frecuentes.
- Calcular el IVA del 19%.
- Generar tickets de venta.
- Consultar el reporte de caja y estadísticas diarias.
- Identificar el producto más vendido.

---

## Requisitos

Para ejecutar este proyecto es necesario tener instalado:

- .NET SDK
- Git

---

## Clonar el repositorio

Para descargar el proyecto desde GitHub, debes de abrir la terminal en vs y ejecutar:

```bash
git clone https://github.com/Nizz0z1/GestorVentasUnidad1.git
```
Despues, en la terminal, vas a elegir el programa:

```bash
cd GestorVentasUnidad1
```

Y por ulitmo lo ejecutas con este codigo:

```bash
dotnet run
```

## Ejemplo de ejecución

```text
====================================================
       SISTEMA GESTOR DE VENTAS E INVENTARIO
====================================================

1. Registrar nuevo producto en inventario
2. Consultar inventario completo
3. Registrar una venta
4. Ver reporte de caja y estadísticas diarias
5. Salir

Seleccione una opción (1-5): 1
```
## Ejemplo de Registro de Producto

```text
====================================================
           REGISTRAR NUEVO PRODUCTO
====================================================

Nombre del producto: Arroz
Precio unitario ($): 5000
Stock inicial: 20

El producto 'Arroz' fue registrado con éxito.

```
## Ejemplo de Consulta de Invetario

```text
====================================================
                INVENTARIO COMPLETO
====================================================
1. Arroz | Precio: $5.000,00 | Stock: 20
2. Leche | Precio: $4.000,00 | Stock: 10
3. Pan | Precio: $2.000,00 | Stock: 3 [ALERTA: BAJO STOCK]
```

## Ejemplo de Venta

```text
====================================================
                  TICKET DE VENTA
====================================================
Producto: Arroz (x3)
Subtotal: $15.000,00
Descuento (10%): -$1.500,00
IVA (19%): +$2.565,00
----------------------------------------------------
TOTAL A PAGAR: $16.065,00
====================================================
```

### Ejemplo del reporte

```text
====================================================
           REPORTE DE CAJA Y ESTADÍSTICAS
====================================================

Fecha: 12/09/2026

Ventas realizadas: 3
Unidades vendidas: 8

Subtotal de ventas:       $40.000,00
Descuentos aplicados:      $4.000,00
IVA generado:              $6.840,00
TOTAL RECAUDADO:           $42.840,00

----------------------------------------------------
PRODUCTO MÁS VENDIDO
----------------------------------------------------

Producto: Arroz
Unidades vendidas: 5

====================================================

```Venta efectuada con éxito. Stock actualizado: 17 unidades.
====================================================
```
