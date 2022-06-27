using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Security;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class VeiculoTeste: IDisposable
    {
        private ITestOutputHelper Output { get; }
        private Veiculo veiculo;
        private Operador operador;
        public VeiculoTeste(ITestOutputHelper output)
        {
            Output = output;
            Output.WriteLine("Execução do  construtor.");
            veiculo = new Veiculo();
            veiculo.Tipo = TipoVeiculo.Automovel;
            operador = new Operador();
            operador.Nome = "Operador Noturno";
        }

        [Fact]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerarCom10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);

        }

        [Fact]
        [Trait("Propriedade", "Proprietário")]
        public void TestaNomeProprietarioVeiculoComDoisCaracteres()
        {
            //Arrange 
            string nomeProprietario = "Ab";
            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaQuantidadeCaracteresPlacaVeiculo()
        {
            //Arrange 
            string placa = "Ab";
            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo().Placa=placa
            );
        }

        [Fact]
        public void TestaQuartoCaractereDaPlaca()
        {
            //Arrange 
            string placa = "ASDF8888";
            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo().Placa = placa
            );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            //Arrange 
            string placa = "ASDF8888";
            //Assert
            var mensagem = Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo().Placa = placa
            );

            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        [Fact]
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFreiarCom10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaAlteraDadosVeiculoDeUmDeterminadoVeiculoComBaseNaPlaca()
        {
            //Arrange

            Patio estacionamento = new Patio();
            estacionamento.OperadorPatio = operador;
            var veiculo = new Veiculo();
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Proprietario = "Jos� Silva";
            veiculo.Placa = "ZXC-8524";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Opala";     
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Tipo = TipoVeiculo.Automovel;
            veiculoAlterado.Proprietario = "Jos� Silva";
            veiculoAlterado.Placa = "ZXC-8524";
            veiculoAlterado.Cor = "Preto"; //Alterado
            veiculoAlterado.Modelo = "Opala";


            //Act
            var alterado = estacionamento.AlteraDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor,veiculoAlterado.Cor);

        }

        [Fact]
        public void TestaTipoVeiculo()
        {
            //Arrange
            var _veiculo = new Veiculo();
            //Assert
            Assert.Equal(TipoVeiculo.Automovel, _veiculo.Tipo);
        }
        
        [Fact]
        public void TestaTipoVeiculoMotocicleta()
        {
            //Arrange
            var _veiculo = new Veiculo();
            _veiculo.Tipo = TipoVeiculo.Motocicleta;
            
            //Assert
            Assert.Equal(TipoVeiculo.Motocicleta, _veiculo.Tipo);
        }
        
        [Theory]
        [ClassData(typeof(Veiculo))]
        public void TestaVeiculoClass(Veiculo modelo)
        {
            //Arrange
            var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);
            modelo.Acelerar(10);

            //Assert
            Assert.Equal(modelo.VelocidadeAtual, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void FichaDeInformacaoDoVeiculo()
        {
            //Arrange
            var carro = new Veiculo();
            carro.Proprietario = "Carlos Silva";
            carro.Tipo = TipoVeiculo.Automovel;
            carro.Placa = "ZAP-7419";
            carro.Cor = "Verde";
            carro.Modelo = "Variante";
            
            //Act
            var dados = carro.ToString();
            
            //Assert
            Assert.Contains("Ficha do Veículo:", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "Ab";

            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaMensagemDeExceçãoDoQuartoCaractereDaPlaca()
        {
            //Arrange
            string placa = "ASDF8888";

            //Act
            var mensagem = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placa
                );
            
            //Assert
            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }
        
        [Fact]
        public void TestaUltimosCaracteresPlacaVeiculoComoNumeros()
        {
            //Arrange
            string placaFormatoErrado = "ASD-995U";

            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Veiculo().Placa= placaFormatoErrado
            );

        }
        
        public void Dispose()
        {
            Output.WriteLine("Dispose invocado.");
        }
    }
}
