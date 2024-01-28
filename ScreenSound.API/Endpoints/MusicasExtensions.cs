using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Modelos;
using ScreenSound.Service;
using ScreenSound.Shared.Modelos.Entities;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointMusicas(this WebApplication app)
        {
            #region Endpoint Músicas
            app.MapGet("/Musicas", ([FromServices] ServiceBase<Musica> service) =>
            {
                var musicaList = service.Listar();
                if (musicaList is null)
                {
                    return Results.NotFound();
                }
                var musicaListResponse = EntityListToResponseList(musicaList);
                return Results.Ok(musicaListResponse);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] ServiceBase<Musica> service, string nome) =>
            {
                var musica = service.BuscarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(EntityToResponse(musica));

            });

            app.MapPost("/Musicas", ([FromServices] ServiceBase<Musica> service, ServiceBase <Genero> serviceGenero,[FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.nome)
                {
                    ArtistaId = musicaRequest.artistaId,
                    AnoLancamento = musicaRequest.anoLancamento,
                    Generos = musicaRequest.generos is not null ? GeneroRequestConverter(musicaRequest.generos, serviceGenero) : new List<Genero>()
                };
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

            app.MapPut("/Musicas", ([FromServices] ServiceBase<Musica> service, [FromBody] MusicaRequestEdit musicaRequestEdit) => {
                var musicaParaAtualizar = service.BuscarPor(a => a.Id == musicaRequestEdit.Id);
                if (musicaParaAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaParaAtualizar.Nome = musicaRequestEdit.nome;
                musicaParaAtualizar.AnoLancamento = musicaRequestEdit.anoLancamento;

                service.Editar(musicaParaAtualizar);
                return Results.Ok();
            });
            #endregion
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, ServiceBase<Genero> serviceGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = serviceGenero.BuscarPor(g => g.Nome.ToUpper().Equals(item.nome.ToUpper()));
                if (genero is not null)
                {
                    listaDeGeneros.Add(genero);
                }
                else
                {
                    listaDeGeneros.Add(entity);
                }
            }

            return listaDeGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.nome, Descricao = genero.descricao };
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
        }
    }
}
