using Microsoft.AspNetCore.Mvc;
using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndPointArtistas(this WebApplication app)
        {
            #region Endpoint Artistas
            app.MapGet("/Artistas", ([FromServices] ServiceBase<Artista> service) =>
            {
                return Results.Ok(service.Listar());
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] ServiceBase<Artista> service, string nome) =>
            {
                var artista = service.ListarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", ([FromServices] ServiceBase<Artista> service, [FromBody] Artista artista) =>
            {
                service.Adicionar(artista);
                return Results.Ok(artista);
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] ServiceBase<Artista> service, int id) =>
            {
                var artista = service.BuscarPor(a => a.Id == id);

                if (artista is null)
                {
                    return Results.NotFound();
                }
                service.Excluir(artista);
                return Results.NoContent();
            });

            app.MapPut("/Artistas", ([FromServices] ServiceBase<Artista> service, [FromBody] Artista artista) =>
            {
                var artistaParaAtualizar = service.BuscarPor(a => a.Id == artista.Id);

                if (artistaParaAtualizar is null)
                {
                    return Results.NotFound();
                }
                artistaParaAtualizar.Nome = artista.Nome is null ? artistaParaAtualizar.Nome : artista.Nome;
                artistaParaAtualizar.Bio = artista.Bio is null ? artistaParaAtualizar.Bio : artista.Bio;
                artistaParaAtualizar.FotoPerfil = artista.FotoPerfil is null ? artistaParaAtualizar.FotoPerfil : artista.FotoPerfil;

                service.Editar(artistaParaAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}
