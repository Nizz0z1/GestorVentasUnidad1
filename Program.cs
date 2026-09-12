using System.Globalization;

class Program
{
    // Formato de moneda
    static readonly CultureInfo CulturaMoneda = new CultureInfo("es-CO");

    static void Main(string[] args)
    {
        List<string> nombres = new List<string>();
        List<decimal> precios = new List<decimal>();
        List<int> stocks = new List<int>();
        List<int> unidadesVendidasPorProducto = new List<int>();

        int totalVentasRealizadas = 0;
        decimal totalCaja = 0m;

        int opcion;

        do
        {
            ImprimirEncabezado("SISTEMA GESTOR DE VENTAS E INVENTARIO");
            Console.WriteLine("1. Registrar nuevo producto en inventario");
            Console.WriteLine("2. Consultar inventario completo");
            Console.WriteLine("3. Registrar una venta");
            Console.WriteLine("4. Ver reporte de caja y estadísticas diarias");
            Console.WriteLine("5. Salir");
            Console.WriteLine("====================================================");

            opcion = LeerEntero("Seleccione una opción (1-5): ", 1, 5);

            switch (opcion)
            {
                case 1:
                    RegistrarProducto(nombres, precios, stocks, unidadesVendidasPorProducto);
                    break;

                case 2:
                    ConsultarInventario(nombres, precios, stocks);
                    break;

                case 3:
                    RegistrarVenta(
                        nombres,
                        precios,
                        stocks,
                        unidadesVendidasPorProducto,
                        ref totalVentasRealizadas,
                        ref totalCaja
                    );
                    break;

                case 4:
                    MostrarReporte(
                        nombres,
                        unidadesVendidasPorProducto,
                        totalVentasRealizadas,
                        totalCaja
                    );
                    break;

                case 5:
                    Console.WriteLine();
                    Console.WriteLine("¡Gracias por usar nuestro servicio. Hasta pronto.");
                    break;
            }

            if (opcion != 5)
            {
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 5);
    }

    // Lee números enteros
    static int LeerEntero(string mensaje, int min, int max)
    {
        int valor;

        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out valor))
            {
                Console.WriteLine("[ERROR] Entrada no válida. Debe ingresar un número entero.");
                continue;
            }

            if (valor < min || valor > max)
            {
                Console.WriteLine($"[ERROR] Opción fuera de rango. Ingrese un valor entre {min} y {max}.");
                continue;
            }

            return valor;
        }
    }

    // Lee números decimales
    static decimal LeerDecimal(string mensaje, decimal min)
    {
        decimal valor;

        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();

            if (!decimal.TryParse(
                entrada,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out valor))
            {
                Console.WriteLine("[ERROR] Entrada no válida. Debe ingresar un número decimal.");
                continue;
            }

            if (valor < min)
            {
                Console.WriteLine($"[ERROR] El valor debe ser mayor o igual a {min}.");
                continue;
            }

            return valor;
        }
    }

    // Calcula el total de la factura
    static decimal CalcularFactura(
        decimal precio,
        int cantidad,
        bool tieneDescuento,
        out decimal montoIva,
        out decimal montoDescuento)
    {
        decimal subtotal = precio * cantidad;

        montoDescuento = tieneDescuento ? subtotal * 0.10m : 0m;

        decimal baseGravable = subtotal - montoDescuento;
        montoIva = baseGravable * 0.19m;

        decimal totalAPagar = baseGravable + montoIva;

        return totalAPagar;
    }

    // Muestra un encabezado
    static void ImprimirEncabezado(string titulo)
    {
        Console.WriteLine("====================================================");
        Console.WriteLine(titulo.ToUpper());
        Console.WriteLine("====================================================");
    }

    // Registra un producto
    static void RegistrarProducto(
        List<string> nombres,
        List<decimal> precios,
        List<int> stocks,
        List<int> unidadesVendidas)
    {
        ImprimirEncabezado("Registrar nuevo producto");

        string nombre;

        while (true)
        {
            Console.Write("Nombre del producto: ");
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("[ERROR] El nombre no puede estar vacío.");
                continue;
            }

            bool tieneNumeros = false;
            foreach (char c in nombre)
            {
                if (char.IsDigit(c))
                {
                    tieneNumeros = true;
                    break;
                }
            }

            if (tieneNumeros)
            {
                Console.WriteLine("[ERROR] El nombre no puede contener números.");
                continue;
            }

            bool yaExiste = false;
            foreach (string n in nombres)
            {
                if (n.Trim().ToLower() == nombre.Trim().ToLower())
                {
                    yaExiste = true;
                    break;
                }   
            }

            if (yaExiste)
            {
                Console.WriteLine("[ERROR] Ya existe un producto registrado con ese nombre.");
                continue;
            }

            break;
        }

        decimal precio = LeerDecimal("Precio unitario ($): ", 0.01m);
        int stock = LeerEntero("Stock inicial (cantidad disponible): ", 0, int.MaxValue);

        nombres.Add(nombre.Trim());
        precios.Add(precio);
        stocks.Add(stock);
        unidadesVendidas.Add(0);

        Console.WriteLine();
        Console.WriteLine($"El Producto '{nombre}' fue registrado con éxito.");
    }

    // Muestra todos los productos
    static void ConsultarInventario(
        List<string> nombres,
        List<decimal> precios,
        List<int> stocks)
    {
        ImprimirEncabezado("Inventario completo");

        if (nombres.Count == 0)
        {
            Console.WriteLine("No hay productos registrados en el inventario.");
            return;
        }

        for (int i = 0; i < nombres.Count; i++)
        {
            string alerta = stocks[i] < 5 ? " [ALERTA: BAJO STOCK]" : "";

            Console.WriteLine(
                $"{i + 1}. {nombres[i],-25} | Precio: {precios[i].ToString("C", CulturaMoneda),12} | Stock: {stocks[i]}{alerta}"
            );
        }
    }

    // Registra una venta
    static void RegistrarVenta(
        List<string> nombres,
        List<decimal> precios,
        List<int> stocks,
        List<int> unidadesVendidas,
        ref int totalVentasRealizadas,
        ref decimal totalCaja)
    {
        ImprimirEncabezado("Registrar venta");

        if (nombres.Count == 0)
        {
            Console.WriteLine("No hay productos registrados. Registre un producto antes de vender.");
            return;
        }

        for (int i = 0; i < nombres.Count; i++)
        {
            string alerta = stocks[i] < 5 ? " [ALERTA: BAJO STOCK]" : "";

            Console.WriteLine(
                $"{i + 1}. {nombres[i],-25} | Precio: {precios[i].ToString("C", CulturaMoneda),12} | Stock: {stocks[i]}{alerta}"
            );
        }

        Console.WriteLine();

        int seleccion = LeerEntero(
            $"Seleccione el número del producto a vender (1-{nombres.Count}): ",
            1,
            nombres.Count
        );

        int indice = seleccion - 1;

        int cantidad;

        while (true)
        {
            cantidad = LeerEntero(
                "Ingrese la cantidad a comprar: ",
                int.MinValue,
                int.MaxValue
            );

            if (cantidad <= 0)
            {
                Console.WriteLine("[ERROR] La cantidad debe ser mayor a cero. Ingrese un número válido.");
                continue;
            }

            if (cantidad > stocks[indice])
            {
                Console.WriteLine(
                    $"[ERROR] Stock insuficiente. Solo quedan {stocks[indice]} unidades en inventario."
                );
                continue;
            }

            break;
        }

        bool aplicaDescuento = false;

        while (true)
        {
            Console.Write("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
            string respuesta = Console.ReadLine();

            if (respuesta != null)
                respuesta = respuesta.Trim().ToUpper();

            if (respuesta == "S")
            {
                aplicaDescuento = true;
                break;
            }
            else if (respuesta == "N")
            {
                aplicaDescuento = false;
                break;
            }
            else
            {
                Console.WriteLine("[ERROR] Respuesta no válida. Ingrese S o N.");
            }
        }

        decimal montoIva;
        decimal montoDescuento;

        decimal total = CalcularFactura(
            precios[indice],
            cantidad,
            aplicaDescuento,
            out montoIva,
            out montoDescuento
        );

        decimal subtotal = precios[indice] * cantidad;

        // Actualiza los datos

        stocks[indice] -= cantidad;
        unidadesVendidas[indice] += cantidad;
        totalVentasRealizadas++;
        totalCaja += total;

        // Muestra el ticket
        
        Console.WriteLine();
        ImprimirEncabezado("Ticket de venta");

        Console.WriteLine($" Producto:             {nombres[indice]} (x{cantidad})");
        Console.WriteLine($" Subtotal:             {subtotal.ToString("C", CulturaMoneda)}");
        Console.WriteLine($" Descuento (10%):     -{montoDescuento.ToString("C", CulturaMoneda)}");
        Console.WriteLine($" IVA (19%):            +{montoIva.ToString("C", CulturaMoneda)}");
        Console.WriteLine(" ---------------------------------------------------");
        Console.WriteLine($" TOTAL A PAGAR:        {total.ToString("C", CulturaMoneda)}");
        Console.WriteLine("====================================================");
        Console.WriteLine(
            $" Venta efectuada con éxito. Stock actualizado: {stocks[indice]} unidades."
        );
    }

    // Muestra el reporte
    static void MostrarReporte(
        List<string> nombres,
        List<int> unidadesVendidas,
        int totalVentasRealizadas,
        decimal totalCaja)
    {
        ImprimirEncabezado("Reporte de caja y estadísticas");

        if (totalVentasRealizadas == 0)
        {
            Console.WriteLine("Aún no se ha registrado ninguna venta en esta sesión.");
            return;
        }

        decimal promedio = totalCaja / totalVentasRealizadas;

        int indiceMasVendido = 0;

        for (int i = 1; i < unidadesVendidas.Count; i++)
        {
            if (unidadesVendidas[i] > unidadesVendidas[indiceMasVendido])
            {
                indiceMasVendido = i;
            }
        }

        Console.WriteLine($"Total de ventas realizadas: {totalVentasRealizadas}");
        Console.WriteLine($"Total acumulado en caja:    {totalCaja.ToString("C", CulturaMoneda)}");
        Console.WriteLine($"Promedio por venta:         {promedio.ToString("C", CulturaMoneda)}");

        if (unidadesVendidas.Count > 0 && unidadesVendidas[indiceMasVendido] > 0)
        {
            Console.WriteLine(
                $"Producto más vendido:       {nombres[indiceMasVendido]} ({unidadesVendidas[indiceMasVendido]} unidades)"
            );
        }
        else
        {
            Console.WriteLine(
                "Producto más vendido:       N/A (sin unidades vendidas aún)"
            );
        }
    }
}