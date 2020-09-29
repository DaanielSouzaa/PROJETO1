using System;

public class Menu
{

	public void Saudacao()
    {
		Console.WriteLine("Olá usuário!");
	}
	public void exibirMenu()
	{
		Console.WriteLine("1 - Cadastro de aluno:");
		Console.WriteLine("----------------------------------------");
		Console.WriteLine("2 - Cadastro de equipamentos:");
		Console.WriteLine("----------------------------------------");
		Console.WriteLine("3 - Equipamentos disponíveis:");
		Console.WriteLine("----------------------------------------");
		Console.WriteLine("4 - Equipamentos emprestados:");
		Console.WriteLine("----------------------------------------");
		Console.WriteLine("5 - Inserir Empréstimo:");
		Console.WriteLine("----------------------------------------");
		Console.WriteLine("6 - Finalizar Empréstimo:");
		Console.WriteLine("----------------------------------------");
	}
}
