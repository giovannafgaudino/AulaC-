using ProjetoLoja.Models;

namespace ProjetoLoja.Interfaces
{
    public interface IClienteRepositorio
    {
        IEnumerable<ClienteViewModel> ListarTudo();
        void CadastrarCliente(ClienteViewModel cliente);   

    }
}
