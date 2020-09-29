using MySqlX.XDevAPI.Common;
using System;
using System.Data;
using System.Data.Common;

namespace Projeto1
{
    class Program        
    {
        static void rodaPrograma ()
        {
            try
            {
                int opcao = 0;
                string result = "";

                Menu menu = new Menu();
                Cadastro cad = new Cadastro();
                Consulta consulta = new Consulta();

                menu.Saudacao();
                Console.WriteLine("----------------------------------------");

                while (opcao < 1 || opcao > 6)
                {
                    menu.exibirMenu();
                    opcao = int.Parse(Console.ReadLine());
                }

                Console.Clear();
                switch (opcao)
                {
                    case 1:
                        cad.Aluno();
                        break;
                    case 2:
                        cad.Equipamento();
                        break;
                    case 3:
                        consulta.equipamentosDisponiveis();
                        Console.WriteLine(result);
                        break;
                    case 4:
                        consulta.equipamentosIndisponiveis();
                        Console.WriteLine(result);
                        break;
                    case 5:
                        cad.criaMovimento();
                        break;
                    case 6:
                        cad.fimMovimento();
                        break;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static void Main(string[] args) { 
            try {
                string sair = "s";

                while (sair == "s")
                {
                    rodaPrograma();

                    Console.WriteLine("Digite 's' para exibir o menu ou qualquer tecla para sair:");
                    sair = Console.ReadLine();
                };
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
