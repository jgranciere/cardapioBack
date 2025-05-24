namespace AprendendoAPI.Models
{
    public interface IProdutoRepository
    {
        void Add(Produto produto);

        List<Produto> Get();

        void Update(Produto produto);

        void Delete(Produto produto);

        Produto BuscarPorId(int id);


    }
}
