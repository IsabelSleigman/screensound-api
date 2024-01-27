using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Executar(ServiceBase<Artista> artista)
    {
        base.Executar(artista);
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        foreach (var elemento in artista.Listar())
        {
            Console.WriteLine($"Artista: {elemento}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
