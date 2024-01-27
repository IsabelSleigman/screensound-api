using ScreenSound.Banco;

namespace ScreenSound.Service
{
    internal class ServiceBase<T> where T : class
    {
        protected readonly ScreenSoundContext _context;
        protected ServiceBase(ScreenSoundContext context)
        {
            _context = context;
        }
        public List<T> Listar()
        {
            return _context.Set<T>().ToList();
        }
        public void Adicionar(T entidade)
        {
            _context.Set<T>().Add(entidade);
            _context.SaveChanges();
        }
        public void Excluir(T entidade)
        {
            _context.Set<T>().Remove(entidade);
            _context.SaveChanges();
        }
        public void Editar(T entidade)
        {
            _context.Set<T>().Update(entidade);
            _context.SaveChanges();
        }
        public T? BuscarPor(Func<T, bool> condicao)
        {
            return _context.Set<T>().FirstOrDefault(condicao);
        }
    }
}
