using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ArtistaService artistaService)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
