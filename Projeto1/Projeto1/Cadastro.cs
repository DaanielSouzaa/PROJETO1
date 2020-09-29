using System;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;

public class Cadastro
{
	public void Aluno()
	{
		Console.WriteLine("1 - Cadastro de aluno:");
		Console.WriteLine("----------------------------------------");
		string aluno = "";
		string matricula = "";

		Console.WriteLine("Qual é o nome do aluno?");
		aluno = Console.ReadLine();

		Console.WriteLine("Qual é a matrícula?");
		matricula = Console.ReadLine();

		try
		{
            using (MySqlConnection conn = new MySqlConnection())
            {
				try
				{
					conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
					conn.Open();
				} catch(Exception e)
                {
					Console.WriteLine("Erro de conexão:" + e);
                }

				MySqlCommand count = conn.CreateCommand();
				count.CommandText = "SELECT COUNT(ID) FROM ALUNOS WHERE (NOME = '"+aluno+"' and MATRICULA = '"+matricula+"') or MATRICULA ='"+matricula+"';";
				count.ExecuteNonQuery();

				MySqlDataReader dr;
				dr = count.ExecuteReader();
				dr.Read();
				int contador = dr.GetInt16(0);
				conn.Close();

				if (contador == 0)
                {
					try {
						conn.Open();
						MySqlCommand command = conn.CreateCommand();
						command.CommandText = "INSERT INTO ALUNOS(NOME,MATRICULA) VALUES('" + aluno + "'," + matricula + ");";
						command.ExecuteNonQuery();
						conn.Close();
						Console.WriteLine("Aluno cadastrado com sucesso!");
					} catch (Exception e){
						Console.WriteLine(e);
                    }
				} else
                {
					Console.WriteLine("Aluno já cadastrado anteriormente!;");
                }
			}
		} catch (Exception e)
        {
			Console.WriteLine(e);
        }

	}

	public void Equipamento()
	{
		Console.WriteLine("2 - Cadastro de equipamentos:");
		Console.WriteLine("----------------------------------------");
		string denominacao = "";
		double custo = 0.0;

		Console.WriteLine("Qual é o nome do equipamento?");
		denominacao = Console.ReadLine();

		Console.WriteLine("Qual foi o custo?");
		custo = double.Parse(Console.ReadLine());

		try
		{
			using (MySqlConnection conn = new MySqlConnection())
			{
				try
				{
					conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
					conn.Open();
				}
				catch (Exception e)
				{
					Console.WriteLine("Erro de conexão:" + e);
				}

				MySqlCommand count = conn.CreateCommand();
				count.CommandText = "SELECT COUNT(ID) FROM EQUIPAMENTOS WHERE DENOMINACAO = '" + denominacao + "';;";
				count.ExecuteNonQuery();

				MySqlDataReader drEquip;
				drEquip = count.ExecuteReader();
				drEquip.Read();
				int contadorEquipamentos = drEquip.GetInt16(0);
				conn.Close();

				if (contadorEquipamentos == 0)
				{
					try
					{
						conn.Open();
						MySqlCommand command = conn.CreateCommand();
						command.CommandText = "INSERT INTO EQUIPAMENTOS(DENOMINACAO,CUSTO) VALUES('" + denominacao + "','"+ custo + "');";
						command.ExecuteNonQuery();
						conn.Close();

						conn.Open();
						count.CommandText = "SELECT ID FROM EQUIPAMENTOS WHERE DENOMINACAO = '" + denominacao + "';";
						count.ExecuteNonQuery();
						MySqlDataReader drID;
						drID = count.ExecuteReader();
						drID.Read();
						int id = drID.GetInt16(0);
						conn.Close();

						conn.Open();
						command.CommandText = "INSERT INTO EQUIP_SALDO(ID_EQUIPAMENTO,SALDO) VALUES('" + id + "',1);";
						command.ExecuteNonQuery();
						conn.Close();
						Console.WriteLine("Equipamento cadastrado com sucesso!");
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
				else
				{
					conn.Open();
					count.CommandText = "SELECT ID FROM EQUIPAMENTOS WHERE DENOMINACAO = '" + denominacao + "';";
					count.ExecuteNonQuery();

					MySqlDataReader drID;
					drID = count.ExecuteReader();
					drID.Read();
					int id = drID.GetInt16(0);
					conn.Close();

					conn.Open();
					MySqlCommand command = conn.CreateCommand();
					command.CommandText = "UPDATE EQUIP_SALDO SET SALDO = SALDO + 1 WHERE ID_EQUIPAMENTO = " + id + ";";
					command.ExecuteNonQuery();
					conn.Close();
					Console.WriteLine("Saldo atualizado com sucesso!");
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	public void exibeAlunos()
    {
		using (MySqlConnection conn = new MySqlConnection())
		{
			try
			{
				conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
			}
			catch (Exception e)
			{
				Console.WriteLine("Erro de conexão:" + e);
			}

			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = "SELECT MATRICULA,NOME FROM ALUNOS";
			MySqlDataReader reader = command.ExecuteReader();

			Console.WriteLine("-------------------------------------------------------------------------");
			while (reader.HasRows)
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (i == 0)
					{
						Console.Write("|		{0}		|", reader.GetName(i));
					}
					else
					{
						Console.Write("		{0}		|", reader.GetName(i));
					}
				};
				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------------------");
				while (reader.Read())
				{
					for (int i = 0; i < reader.FieldCount; i++)
					{
						if (i == 0)
						{
							Console.Write("|		{0}			|", reader.GetString(i));
						}
						else
						{
							Console.Write("		{0}		|", reader.GetString(i));
						}
					};
					Console.WriteLine();
					Console.WriteLine("-------------------------------------------------------------------------");
				}

				reader.NextResult();
			}

			conn.Close();
		}
	}

	public int exibeAlunosSelecionado(string matricula)
	{
		using (MySqlConnection conn = new MySqlConnection())
		{
			try
			{
				conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
			}
			catch (Exception e)
			{
				Console.WriteLine("Erro de conexão:" + e);
			}

			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = "SELECT ID FROM ALUNOS WHERE MATRICULA = "+matricula+"";
			MySqlDataReader reader = command.ExecuteReader();
			reader.Read();
			int idAluno = reader.GetInt16(0);
			conn.Close();
			return idAluno;

		}
	}


	public void exibeEquipamentos()
    {
		using (MySqlConnection conn = new MySqlConnection())
		{
			try
			{
				conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
			}
			catch (Exception e)
			{
				Console.WriteLine("Erro de conexão:" + e);
			}

			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = "SELECT ID,DENOMINACAO FROM EQUIPAMENTOS";
			MySqlDataReader reader = command.ExecuteReader();

			Console.WriteLine("-------------------------------------------------------------------------");
			while (reader.HasRows)
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (i == 0)
					{
						Console.Write("|		{0}		|", reader.GetName(i));
					}
					else
					{
						Console.Write("		{0}		|", reader.GetName(i));
					}
				};
				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------------------");
				while (reader.Read())
				{
					for (int i = 0; i < reader.FieldCount; i++)
					{
						if (i == 0)
						{
							Console.Write("|		{0}			|", reader.GetString(i));
						}
						else
						{
							Console.Write("		{0}		|", reader.GetString(i));
						}
					};
					Console.WriteLine();
					Console.WriteLine("-------------------------------------------------------------------------");
				}

				reader.NextResult();
			}

			conn.Close();
		}
	}
	public void criaMovimento()
	{
		Console.WriteLine("5 - Inserir Empréstimo:");
		Console.WriteLine("----------------------------------------");

		exibeAlunos();

		Console.WriteLine("Digite a matrícula desejada:");
		string matricula = Console.ReadLine();

		int idAluno = exibeAlunosSelecionado(matricula); 

		exibeEquipamentos();

		Console.WriteLine("Digite o ID do equipamento desejado:");
		int idEquipamento = int.Parse(Console.ReadLine());

		using (MySqlConnection conn = new MySqlConnection())
		{
			try
			{
				conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
			}
			catch (Exception e)
			{
				Console.WriteLine("Erro de conexão:" + e);
			}

			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = "INSERT REG_MOV(ID_ALUNO,ID_EQUIPAMENTO,DATA_ENTREGA) VALUES('" + idAluno + "','" + idEquipamento + "',NOW());";
			command.ExecuteNonQuery();
			conn.Close();

			conn.Open();
			MySqlCommand commandSaldo = conn.CreateCommand();
			commandSaldo.CommandText = "UPDATE EQUIP_SALDO SET SALDO = SALDO - 1 WHERE ID = '"+idEquipamento+"';";
			commandSaldo.ExecuteNonQuery();
			conn.Close();

			Console.WriteLine("Equipamento emprestado com sucesso!");
		}

	}

	public void exibeMovimentos()
	{
		using (MySqlConnection conn = new MySqlConnection())
		{
			try
			{
				conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
			}
			catch (Exception e)
			{
				Console.WriteLine("Erro de conexão:" + e);
			}

			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = "SELECT RM.ID,AL.NOME,E.DENOMINACAO,DATA_ENTREGA,ifnull(DATA_DEVOLUCAO,'0000-00-00 00:00:00') AS DATA_DEVOLUCAO FROM emprestaarduino.reg_mov AS RM INNER JOIN EQUIPAMENTOS AS E ON E.ID = RM.ID_EQUIPAMENTO INNER JOIN ALUNOS AS AL ON AL.ID = RM.ID_ALUNO WHERE DATA_DEVOLUCAO IS NULL";
			MySqlDataReader reader = command.ExecuteReader();

			Console.WriteLine("-------------------------------------------------------------------------");
			while (reader.HasRows)
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (i == 0)
					{
						Console.Write("|{0}|", reader.GetName(i));
					}
					else
					{
						Console.Write("{0}|", reader.GetName(i));
					}
				};
				Console.WriteLine();
				Console.WriteLine("-------------------------------------------------------------------------");
				while (reader.Read())
				{
					for (int i = 0; i < reader.FieldCount; i++)
					{
						if (i == 0)
						{
							Console.Write("|{0}|", reader.GetString(i));
						}
						else
						{
							Console.Write("{0}|", reader.GetString(i));
						}
					};
					Console.WriteLine();
					Console.WriteLine("-------------------------------------------------------------------------");
				}

				reader.NextResult();
			}

			conn.Close();
		}
	}

	public void fimMovimento()
	{
		string resposta = "";

		Console.WriteLine("6 - Finalizar Empréstimo:");
		Console.WriteLine("----------------------------------------");

		exibeMovimentos();

		Console.WriteLine("Digite o id do empréstimo que será finalizado:");
		int idMov = int.Parse(Console.ReadLine());

		Console.WriteLine("Tem certeza que deseja encerrar o movimento?");
		resposta = Console.ReadLine();

		if (resposta == "s"){ 
			using (MySqlConnection conn = new MySqlConnection())
			{
				try
				{
					conn.ConnectionString = "server=localhost;database=EmprestaArduino;uid=root;password=;";
				}
				catch (Exception e)
				{
					Console.WriteLine("Erro de conexão:" + e);
				}

				conn.Open();
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = "UPDATE REG_MOV SET DATA_DEVOLUCAO = NOW() WHERE ID = '" + idMov + "';";
				command.ExecuteNonQuery();
				conn.Close();

				conn.Open();
				MySqlCommand commandAluno = conn.CreateCommand();
				command.CommandText = "SELECT ID_EQUIPAMENTO FROM REG_MOV WHERE ID = " + idMov + "";
				MySqlDataReader reader = command.ExecuteReader();
				reader.Read();
				int idEquipamento = reader.GetInt16(0);

				conn.Open();
				MySqlCommand commandSaldo = conn.CreateCommand();
				commandSaldo.CommandText = "UPDATE EQUIP_SALDO SET SALDO = SALDO + 1 WHERE ID = '"+idEquipamento+"'";
				commandSaldo.ExecuteNonQuery();
				conn.Close();

				Console.WriteLine("Movimento finalizado com sucesso!");
			}
		}

	}
}
