using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Estacionamento.Modelos
{
    public class Operador
    {
        // Campos
        private string _matricula;
        private string _nome;

        //Propriedades
        public string Matricula { get => _matricula;  private set => _matricula = value; }
        public string Nome { get => _nome; set => _nome = value; }

        //Construtor
        public Operador()
        {
            Matricula = Guid.NewGuid().ToString().Substring(0,8);
        }

        public override string ToString()
        {
            return $"Operador: {Nome} \n" +
                   $"Matricula: {Matricula}";
        }
    }
}
