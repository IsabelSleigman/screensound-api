using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ServiceBase<Artista> artista)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
