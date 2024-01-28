using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointMusicas(this WebApplication app)
        {
            #region Endpoint Músicas
            app.MapGet("/Musicas", ([FromServices] ServiceBase<Musica> service) =>
            {
                return Results.Ok(service.Listar());
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] ServiceBase<Musica> service, string nome) =>
            {
                var musica = service.BuscarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(musica);

            });

            app.MapPost("/Musicas", ([FromServices] ServiceBase<Musica> service, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.nome);
                service.Adicionar(musica);
                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] ServiceBase<Musica> service, int id) => {
                var musica = service.BuscarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                service.Excluir(musica);
                return Results.NoContent();

            });

            app.MapPut("/Musicas", ([FromServices] ServiceBase<Musica> service, [FromBody] Musica musica) => {
                var musicaParaAtualizar = service.BuscarPor(a => a.Id == musica.Id);
                if (musicaParaAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaParaAtualizar.Nome = musica.Nome is null ? musicaParaAtualizar.Nome : musica.Nome;
                musicaParaAtualizar.AnoLancamento = musica.AnoLancamento is null ? musicaParaAtualizar.AnoLancamento : musica.AnoLancamento;

                service.Editar(musicaParaAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}
