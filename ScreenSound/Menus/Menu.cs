﻿using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.Menus;

internal class Menu
{
    public void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }
    public virtual void Executar(ServiceBase<Artista> artista)
    {
        Console.Clear();
    }
}
