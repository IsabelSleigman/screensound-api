using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(ServiceBase<Artista> artista)
    {
        base.Executar(artista);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaPesquisado = artista.BuscarPor(a => a.Nome == nomeDoArtista);
        if (artistaPesquisado is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            Console.Write("Agora digite o ano de lançamento da música: ");
            string anoLancamento = Console.ReadLine()!;
            artistaPesquisado.AdicionarMusica(new Musica(tituloDaMusica) { AnoLancamento = Convert.ToInt32(anoLancamento)});
            Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");
            artista.Editar(artistaPesquisado);
            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
