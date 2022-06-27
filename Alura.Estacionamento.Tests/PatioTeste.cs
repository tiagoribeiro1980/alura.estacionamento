using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class PatioTeste: IDisposable
    {
        
        private ITestOutputHelper Output { get; }
        private Veiculo veiculo;
        private Operador operador;

        public PatioTeste(ITestOutputHelper output)
        {
            Output = output;
            Output.WriteLine("Execução do  construtor.");
            veiculo = new Veiculo();
            operador  = new Operador
            {
                Nome = "Operador noturno"
            };
        }
        [Fact]
        public void ValidaFaturamentoDeSomenteUmVeiculoPatio()
        {
            //Arranje
            var estacionamento = new Patio
            {
                OperadorPatio = operador
            };
            
            veiculo.Proprietario = "André Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "ABC-0101";
            veiculo.Modelo = "Fusca";    
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            var faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "preto", "Gol")]
        [InlineData("Jose Silva", "POL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GDR-6524", "Azul", "Opala")]
        public void ValidaFaturamentoComVariosVeiculosNoPatio(string proprietario,
                                                        string placa,
                                                        string cor,
                                                        string modelo)
        {
            //Arranje
            var estacionamento = new Patio
            {
                OperadorPatio = operador
            };
            
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNaPlaca(string proprietario,
                                           string placa,
                                           string cor,
                                           string modelo)
        {
            //Arrange
            var estacionamento = new Patio
            {
                OperadorPatio = operador
            };
            
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculoPorPlaca(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNoTicket(string proprietario,
                                          string placa,
                                          string cor,
                                          string modelo)
        {
            //Arrange
            var estacionamento = new Patio
            {
                OperadorPatio = operador
            };

            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculoPorTicket(veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionameno Alura ###", consultado.Ticket);
        }

        [Fact]
        public void AlterarDadosDoProprioVeiculo()
        {
            //Arranje
            var estacionamento = new Patio();

            veiculo.Proprietario = "José Silva";
            veiculo.Placa = "ZXC-8524";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Opala";
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "José Silva";
            veiculoAlterado.Placa = "ZXC-8524";
            veiculoAlterado.Cor = "Preto"; //Alterado
            veiculoAlterado.Modelo = "Opala";

            //Act
            var alterado = estacionamento.AlteraDadosVeiculo(veiculoAlterado);
            
            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            Output.WriteLine("Dispose invocado.");
        }
    }
}
