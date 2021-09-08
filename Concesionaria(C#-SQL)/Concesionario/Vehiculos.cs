using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionario
{
    class Vehiculos
    {
        int codigo;
        int modelo;
        int marca;
        double precio;
        int puertas;
        int color;

        public Vehiculos()
        {
            codigo = 0;
            modelo = 0;
            marca = 0;
            precio = 0;
            puertas = 0;
            color = 0;
        }
        public Vehiculos(int codigo, int modelo, int marca, double precio, int puertas, int color)
        {
            this.codigo = codigo;
            this.modelo = modelo;
            this.marca = marca;
            this.precio = precio;
            this.puertas = puertas;
            this.color = color;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public int Modelo { get => modelo; set => modelo = value; }
        public int Marca { get => marca; set => marca = value; }
        public double Precio { get => precio; set => precio = value; }
        public int Puertas { get => puertas; set => puertas = value; }
        public int Color { get => color; set => color = value; }

        public override string ToString()
        {
            return codigo + " - " + marca;
        }
    }
}
