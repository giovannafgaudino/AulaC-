using MySql.Data.MySqlClient;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Models;

namespace ProjetoLoja.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string _connectionString;

        public ClienteRepositorio(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Conexao");
        }

        public IEnumerable<ClienteViewModel> ListarTudo()
        {
            var clientes = new List<ClienteViewModel>();

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var cmd = new MySqlCommand("SELECT Id, Nome, Telefone, Email FROM Clientes ORDER BY Nome", con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                clientes.Add(new ClienteViewModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString()!,
                    Telefone = reader["Telefone"].ToString()!,
                    Email = reader["Email"].ToString()!
                });
            }

            return clientes;
        }

        public void CadastrarCliente(ClienteViewModel cliente)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var cmd = new MySqlCommand(
                @"INSERT INTO Clientes (Nome, Telefone, Email)
                  VALUES (@nome, @telefone, @email)", con);

            cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@email", cliente.Email);

            cmd.ExecuteNonQuery();
        }
    }
}