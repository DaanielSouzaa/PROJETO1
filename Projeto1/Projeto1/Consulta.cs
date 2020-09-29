using System;
using System.Data;
using MySql.Data.MySqlClient;

public class Consulta
{
	public void equipamentosDisponiveis()
	{
		try
        {
			Console.WriteLine("3 - Equipamentos disponíveis:");
			Console.WriteLine("----------------------------------------");
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
				command.CommandText = "SELECT E.DENOMINACAO,ES.SALDO FROM EQUIPAMENTOS AS E INNER JOIN EQUIP_SALDO AS ES ON ES.ID_EQUIPAMENTO = E.ID WHERE ES.SALDO > 0;";
				MySqlDataReader reader = command.ExecuteReader();

				Console.WriteLine("-------------------------------------------------------------------------");
				while (reader.HasRows)
				{
					for (int i = 0; i < reader.FieldCount; i++)
					{
						if(i == 0) { 
							Console.Write("|		{0}		|", reader.GetName(i));
						} else
                        {
							Console.Write("		{0}		|", reader.GetName(i));
						}
					};
					Console.WriteLine();
					Console.WriteLine("-------------------------------------------------------------------------");
					while (reader.Read()) {
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

		} catch (Exception e)
        {
			Console.WriteLine(e);
		}
	}
	public void equipamentosIndisponiveis()
	{
		try
		{
			Console.WriteLine("4 - Equipamentos emprestados:");
			Console.WriteLine("----------------------------------------");
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
				command.CommandText = "SELECT count(RM.ID) FROM emprestaarduino.reg_mov AS RM INNER JOIN EQUIPAMENTOS AS E ON E.ID=RM.ID_EQUIPAMENTO WHERE DATA_DEVOLUCAO IS NULL GROUP BY ID_EQUIPAMENTO;";
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
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}
