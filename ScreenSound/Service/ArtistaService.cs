using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Service
{
    internal class ArtistaService
    {
        private readonly ScreenSoundContext _context;

        public ArtistaService(ScreenSoundContext context)
        {
            _context = context;
        }

        public List<Artista> Listar()
        {
            var listaArtistas = _context.Artistas.ToList();

            _ = listaArtistas ?? throw new Exception("Nenhum artista encontrado!");

            return listaArtistas;
        }
        public Artista BuscarArtista(string nomeArtista)
        {
            var artista = _context.Artistas.FirstOrDefault(a => a.Nome == nomeArtista);

            _ = artista ?? throw new Exception("Artista não encontrado!");

            return artista;
        }
        public void Adicionar(Artista artista)
        {
            _context.Artistas.Add(artista);
            _context.SaveChanges();
        }
        public void Editar(Artista artista)
        {
            _context.Artistas.Update(artista);
            _context.SaveChanges();

        }
        public void Deletar(Artista artista)
        {
            _context.Artistas.Remove(artista);
            _context.SaveChanges();

        }
    }
}
