using System;

class Metodos
{
    static void Main()
    {
        inicio:
        Console.Clear();

        #region Entrada do Tamanho do Campo
        entradaAltura:
        Console.WriteLine("Insira a altura do campo (Mínimo:9; Máximo: 30): ");
        int altura = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        if (altura < 9 || altura > 30)
        {
            Console.WriteLine("Valor Inválido!");
            goto entradaAltura;
        }

        entradaLargura:
        Console.WriteLine("Insira a largura do campo (Mínimo:9; Máximo: 30): ");
        int largura = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        if (largura < 9 || largura > 30)
        {
            Console.WriteLine("Valor Inválido!");
            goto entradaLargura;
        }

        entradaDificuldade:
        Console.Write("Dificuldades:\nFácil(1)\nMédio(2)\nDifícil(3)\nInsano(4)\nEscolha: ");
        int dificuldade = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        if (dificuldade != 1 && dificuldade != 2 && dificuldade != 3 && dificuldade != 4)
        {
            Console.WriteLine("Valor Inválido!");
            goto entradaDificuldade;
        }

        int numMinas = 0;
        switch (dificuldade)
        {
            case 1:
                numMinas = Convert.ToInt32(Math.Floor(altura * largura / 8.1));
                break;
            case 2:
                numMinas = Convert.ToInt32(Math.Floor(altura * largura / 6.4));
                break;
            case 3:
                numMinas = Convert.ToInt32(Math.Floor(altura * largura / 4.84));
                break;
            case 4:
                numMinas = Convert.ToInt32(Math.Floor(altura * largura / 3.2));
                break;
        }
        #endregion

        #region Declarações
        Random rand = new Random();
        int[,] campoFormado = new int[altura, largura];
        string[,] campoFormatado = new string[altura, largura];
        string[,] campoFechado = new string[altura, largura];
        int alturaMax = campoFormado.GetLength(0);
        int larguraMax = campoFormado.GetLength(1);
        int[] minasPorLinha = new int[altura];
        int numMinasPorLinha = numMinas / altura;
        int numMinasAoRedor;
        int cont;
        int a = 0;
        int l = 0;
        int alturaCelulaSelecionada = 0;
        int larguraCelulaSelecionada = 0;
        string operador;
        bool matriz1IgualAMatriz2 = false;
        #endregion

        #region Definição das Minas por Linha
        for (a = 0; a < alturaMax; a++)
        {
            minasPorLinha[a] = Convert.ToInt32(Math.Floor(Convert.ToDouble(numMinas / altura)));
        }
        if (numMinas % altura != 0)
        {
            int restoMinasPorLinha = numMinas % altura;
            for (cont = 0; cont < restoMinasPorLinha; cont++)
            {
                int linhaAleatória = rand.Next(0, alturaMax - 1);
                minasPorLinha[linhaAleatória]++;
            }
        }
        #endregion

        #region Definição da Posição das Minas
        for (a = 0; a < alturaMax; a++)
        {
            int[] celulasComMina = new int[minasPorLinha[a]];
            for (cont = 0; cont < minasPorLinha[a]; cont++)
            {
                celulasComMina[cont] = rand.Next(0, larguraMax - 1);
                campoFormado[a, celulasComMina[cont]] = 9;
            }
        }
        #endregion

        #region Definição das Células
        operador = "igual";
        for (a = 0; a < alturaMax; a++)
        {
            for (l = 0; l < larguraMax; l++)
            {
                if (campoFormado[a, l] == 0)
                {
                    numMinasAoRedor = 0;
                    campoFormado[a, l] = DefinirNumeroDeMinasAoRedor(
                        campoFormado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        0,
                        9,
                        numMinasAoRedor,
                        operador
                    );
                }
            }
        }
        #endregion

        #region Formatação do Campo
        for (a = 0; a < alturaMax; a++)
        {
            for (l = 0; l < larguraMax; l++)
            {
                switch (campoFormado[a, l])
                {
                    case 0:
                        campoFormatado[a, l] = "|*|";
                        break;
                    case 1:
                        campoFormatado[a, l] = "|1|";
                        break;
                    case 2:
                        campoFormatado[a, l] = "|2|";
                        break;
                    case 3:
                        campoFormatado[a, l] = "|3|";
                        break;
                    case 4:
                        campoFormatado[a, l] = "|4|";
                        break;
                    case 5:
                        campoFormatado[a, l] = "|5|";
                        break;
                    case 6:
                        campoFormatado[a, l] = "|6|";
                        break;
                    case 7:
                        campoFormatado[a, l] = "|7|";
                        break;
                    case 8:
                        campoFormatado[a, l] = "|8|";
                        break;
                    case 9:
                        campoFormatado[a, l] = "|X|";
                        break;
                }
            }
        }
        #endregion

        #region Definição do Campo fechado
        for (a = 0; a < alturaMax; a++)
        {
            for (l = 0; l < larguraMax; l++)
            {
                if (campoFormado[a, l] == 0)
                {
                    campoFechado[a, l] = campoFormatado[a, l];
                    operador = "diferente";
                    switch (RetornaCantoDeUmaCelula(a, l, alturaMax, larguraMax))
                    {
                        case 1:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    5
                                )
                            )
                                campoFechado[a, l + 1] = campoFormatado[a, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    7
                                )
                            )
                                campoFechado[a + 1, l] = campoFormatado[a + 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    8
                                )
                            )
                                campoFechado[a + 1, l + 1] = campoFormatado[a + 1, l + 1];
                            break;
                        case 2:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    4
                                )
                            )
                                campoFechado[a, l - 1] = campoFormatado[a, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    5
                                )
                            )
                                campoFechado[a, l + 1] = campoFormatado[a, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    6
                                )
                            )
                                campoFechado[a + 1, l - 1] = campoFormatado[a + 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    7
                                )
                            )
                                campoFechado[a + 1, l] = campoFormatado[a + 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    8
                                )
                            )
                                campoFechado[a + 1, l + 1] = campoFormatado[a + 1, l + 1];
                            break;
                        case 3:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    4
                                )
                            )
                                campoFechado[a, l - 1] = campoFormatado[a, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    6
                                )
                            )
                                campoFechado[a + 1, l - 1] = campoFormatado[a + 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    7
                                )
                            )
                                campoFechado[a + 1, l] = campoFormatado[a + 1, l];
                            break;
                        case 4:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    2
                                )
                            )
                                campoFechado[a - 1, l] = campoFormatado[a - 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    3
                                )
                            )
                                campoFechado[a - 1, l + 1] = campoFormatado[a - 1, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    5
                                )
                            )
                                campoFechado[a, l + 1] = campoFormatado[a, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    7
                                )
                            )
                                campoFechado[a + 1, l] = campoFormatado[a + 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    8
                                )
                            )
                                campoFechado[a + 1, l + 1] = campoFormatado[a + 1, l + 1];
                            break;
                        case 5:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    1
                                )
                            )
                                campoFechado[a - 1, l - 1] = campoFormatado[a - 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    2
                                )
                            )
                                campoFechado[a - 1, l] = campoFormatado[a - 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    4
                                )
                            )
                                campoFechado[a, l - 1] = campoFormatado[a, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    6
                                )
                            )
                                campoFechado[a + 1, l - 1] = campoFormatado[a + 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    7
                                )
                            )
                                campoFechado[a + 1, l] = campoFormatado[a + 1, l];
                            break;
                        case 6:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    2
                                )
                            )
                                campoFechado[a - 1, l] = campoFormatado[a - 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    3
                                )
                            )
                                campoFechado[a - 1, l + 1] = campoFormatado[a - 1, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    5
                                )
                            )
                                campoFechado[a, l + 1] = campoFormatado[a, l + 1];
                            break;
                        case 7:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    1
                                )
                            )
                                campoFechado[a - 1, l - 1] = campoFormatado[a - 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    2
                                )
                            )
                                campoFechado[a - 1, l] = campoFormatado[a - 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    3
                                )
                            )
                                campoFechado[a - 1, l + 1] = campoFormatado[a - 1, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    4
                                )
                            )
                                campoFechado[a, l - 1] = campoFormatado[a, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    5
                                )
                            )
                                campoFechado[a, l + 1] = campoFormatado[a, l + 1];
                            break;
                        case 8:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    1
                                )
                            )
                                campoFechado[a - 1, l - 1] = campoFormatado[a - 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    2
                                )
                            )
                                campoFechado[a - 1, l] = campoFormatado[a - 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    4
                                )
                            )
                                campoFechado[a, l - 1] = campoFormatado[a, l - 1];
                            break;
                        case 9:
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    1
                                )
                            )
                                campoFechado[a - 1, l - 1] = campoFormatado[a - 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    2
                                )
                            )
                                campoFechado[a - 1, l] = campoFormatado[a - 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    3
                                )
                            )
                                campoFechado[a - 1, l + 1] = campoFormatado[a - 1, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    4
                                )
                            )
                                campoFechado[a, l - 1] = campoFormatado[a, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    5
                                )
                            )
                                campoFechado[a, l + 1] = campoFormatado[a, l + 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    6
                                )
                            )
                                campoFechado[a + 1, l - 1] = campoFormatado[a + 1, l - 1];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    7
                                )
                            )
                                campoFechado[a + 1, l] = campoFormatado[a + 1, l];
                            if (
                                CelulaNumCantoEhIgualOuDiferenteDeNumero(
                                    campoFormado,
                                    a,
                                    l,
                                    0,
                                    operador,
                                    8
                                )
                            )
                                campoFechado[a + 1, l + 1] = campoFormatado[a + 1, l + 1];
                            break;
                    }
                }
                else if (campoFechado[a, l] == null)
                    campoFechado[a, l] = "|_|";
            }
        }
        #endregion

        #region Definição dos Controles
        Console.Clear();
        a = 0;
        l = 0;
        PrintarCelulaSelecionada(
            campoFechado,
            a,
            l,
            alturaMax,
            larguraMax,
            alturaCelulaSelecionada,
            larguraCelulaSelecionada
        );
        do
        {
            Matriz1DiferenteDeMatriz2:
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    Console.Clear();
                    if (alturaCelulaSelecionada >= 0)
                    {
                        alturaCelulaSelecionada--;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.UpArrow:
                    Console.Clear();
                    if (alturaCelulaSelecionada >= 0)
                    {
                        alturaCelulaSelecionada--;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.A:
                    Console.Clear();
                    if (larguraCelulaSelecionada >= 0)
                    {
                        larguraCelulaSelecionada--;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Clear();
                    if (larguraCelulaSelecionada >= 0)
                    {
                        larguraCelulaSelecionada--;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.S:
                    Console.Clear();
                    if (alturaCelulaSelecionada < alturaMax - 1)
                    {
                        alturaCelulaSelecionada++;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.DownArrow:
                    Console.Clear();
                    if (alturaCelulaSelecionada < alturaMax - 1)
                    {
                        alturaCelulaSelecionada++;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.D:
                    Console.Clear();
                    if (larguraCelulaSelecionada < larguraMax - 1)
                    {
                        larguraCelulaSelecionada++;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.RightArrow:
                    Console.Clear();
                    if (larguraCelulaSelecionada < larguraMax - 1)
                    {
                        larguraCelulaSelecionada++;
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.K:
                    Console.Clear();
                    if (campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] == "|_|")
                    {
                        if (
                            campoFormatado[alturaCelulaSelecionada, larguraCelulaSelecionada]
                            == "|X|"
                        )
                        {
                            goto derrota;
                        }
                        else
                        {
                            campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] =
                                campoFormatado[alturaCelulaSelecionada, larguraCelulaSelecionada];
                        }
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.L:
                    Console.Clear();
                    if (campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] == "|_|")
                    {
                        campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] = "|X|";
                    }
                    else if (
                        campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] == "|X|"
                    )
                    {
                        campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] = "|_|";
                    }
                    else
                    {
                        campoFechado[alturaCelulaSelecionada, larguraCelulaSelecionada] =
                            campoFormatado[alturaCelulaSelecionada, larguraCelulaSelecionada];
                    }
                    PrintarCelulaSelecionada(
                        campoFechado,
                        a,
                        l,
                        alturaMax,
                        larguraMax,
                        alturaCelulaSelecionada,
                        larguraCelulaSelecionada
                    );
                    break;
                case ConsoleKey.Escape:
                    return;
            }

            while (a < alturaMax)
            {
                TesteLargura:
                while (l < larguraMax)
                {
                    if (campoFechado[a, l] == campoFormatado[a, l])
                    {
                        l++;
                        goto TesteLargura;
                    }
                    else
                    {
                        goto Matriz1DiferenteDeMatriz2;
                    }
                }
                a++;
                l = 0;
            }
            matriz1IgualAMatriz2 = false;
        } while (matriz1IgualAMatriz2);
        goto vitória;
        #endregion

        #region Telas Finais
        derrota:
        Console.Clear();
        PrintarMatriz(campoFormatado, a, l, alturaMax, larguraMax);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nVOCÊ PERDEU :(");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Pressione Esc para fechar o jogo ou R para jogar novamente!");
        while (true)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.R:
                    Console.ResetColor();
                    goto inicio;
            }
        }

        vitória:
        Console.Clear();
        PrintarMatriz(campoFormatado, a, l, alturaMax, larguraMax);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nPARABÉNS, VOCÊ GANHOU!");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Pressione Esc para fechar o jogo ou R para jogar novamente!)");
        while (true)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.R:
                    Console.ResetColor();
                    goto inicio;
            }
        }
        #endregion
    }

    private static int CalculaValorDaCelulaNumCanto(
        int[,] matriz,
        int indice1,
        int indice2,
        int numeroCanto
    )
    {
        switch (numeroCanto)
        {
            case 1:
                return matriz[indice1 - 1, indice2 - 1];

            case 2:
                return matriz[indice1 - 1, indice2];

            case 3:
                return matriz[indice1 - 1, indice2 + 1];

            case 4:
                return matriz[indice1, indice2 - 1];

            case 5:
                return matriz[indice1, indice2 + 1];

            case 6:
                return matriz[indice1 + 1, indice2 - 1];

            case 7:
                return matriz[indice1 + 1, indice2];

            case 8:
                return matriz[indice1 + 1, indice2 + 1];

            default:
                return -1;
        }
    }

    public static bool CelulaNumCantoEhIgualOuDiferenteDeNumero(
        int[,] matriz,
        int indice1,
        int indice2,
        int numero,
        string operador,
        int numeroCanto
    )
    {
        int valorDaCelulaNumCanto;
        switch (operador)
        {
            case "igual":
                valorDaCelulaNumCanto = CalculaValorDaCelulaNumCanto(
                    matriz,
                    indice1,
                    indice2,
                    numeroCanto
                );
                return valorDaCelulaNumCanto == numero;
            case "diferente":
                valorDaCelulaNumCanto = CalculaValorDaCelulaNumCanto(
                    matriz,
                    indice1,
                    indice2,
                    numeroCanto
                );
                return valorDaCelulaNumCanto != numero;
            default:
                return false;
        }
    }

    public static int RetornaCantoDeUmaCelula(
        int indice1,
        int indice2,
        int tamanhoindice1,
        int tamanhoindice2
    )
    {
        if (indice1 == 0 && indice2 == 0)
        {
            return 1;
        }
        else if (indice1 == 0 && indice2 > 0 && indice2 < tamanhoindice2 - 1)
        {
            return 2;
        }
        else if (indice1 == 0 && indice2 == tamanhoindice2 - 1)
        {
            return 3;
        }
        else if (indice2 == 0 && indice1 > 0 && indice1 < tamanhoindice1 - 1)
        {
            return 4;
        }
        else if (indice2 == tamanhoindice2 - 1 && indice1 > 0 && indice1 < tamanhoindice1 - 1)
        {
            return 5;
        }
        else if (indice1 == tamanhoindice1 - 1 && indice2 == 0)
        {
            return 6;
        }
        else if (indice1 == tamanhoindice1 - 1 && indice2 > 0 && indice2 < tamanhoindice2 - 1)
        {
            return 7;
        }
        else if (indice1 == tamanhoindice1 - 1 && indice2 == tamanhoindice2 - 1)
        {
            return 8;
        }
        else
        {
            return 9;
        }
    }

    public static int RetornaNumeroDeMinasDeUmaCelula(
        int[,] matriz,
        int indice1,
        int indice2,
        int numero,
        int numeroDeMinas,
        string operador,
        int cantoDaCelula
    )
    {
        switch (cantoDaCelula)
        {
            case 1:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        5
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        7
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        8
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 2:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        4
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        5
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        6
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        7
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        8
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 3:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        4
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        6
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        7
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 4:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        2
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        3
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        5
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        7
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        8
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 5:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        1
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        2
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        4
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        6
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        7
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 6:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        2
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        3
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        5
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 7:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        1
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        2
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        3
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        4
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        5
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 8:
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        1
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        2
                    )
                )
                    numeroDeMinas++;
                if (
                    CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        4
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            case 9:
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        1
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        2
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        3
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        4
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        5
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        6
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        7
                    )
                )
                    numeroDeMinas++;
                if (
                    Metodos.CelulaNumCantoEhIgualOuDiferenteDeNumero(
                        matriz,
                        indice1,
                        indice2,
                        numero,
                        operador,
                        8
                    )
                )
                    numeroDeMinas++;
                return numeroDeMinas;
            default:
                return -1;
        }
    }

    public static int DefinirNumeroDeMinasAoRedor(
        int[,] matriz,
        int indice1,
        int indice2,
        int tamanhoindice1,
        int tamanhoindice2,
        int numero1,
        int numero2,
        int numeroDeMinas,
        string operador
    )
    {
        var cantoDaCelula = RetornaCantoDeUmaCelula(
            indice1,
            indice2,
            tamanhoindice1,
            tamanhoindice2
        );
        var numeroMinas = RetornaNumeroDeMinasDeUmaCelula(
            matriz,
            indice1,
            indice2,
            numero2,
            numeroDeMinas,
            operador,
            cantoDaCelula
        );
        return numeroMinas;
    }

    public static void PrintarMatriz(
        string[,] matriz,
        int indice1,
        int indice2,
        int tamanhoindice1,
        int tamanhoindice2
    )
    {
        for (indice1 = 0; indice1 < tamanhoindice1; indice1++)
        {
            for (indice2 = 0; indice2 < tamanhoindice2; indice2++)
            {
                switch (matriz[indice1, indice2])
                {
                    case "|*|":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|1|":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|2|":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|3|":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|4|":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|5|":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|6|":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|7|":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|8|":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|X|":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(matriz[indice1, indice2]);
                        break;
                }
                if (matriz[indice1, indice2] == "|_|")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(matriz[indice1, indice2]);
                }
            }
            Console.WriteLine();
        }
    }

    public static void PrintarCelulaSelecionada(
        string[,] matriz,
        int indice1,
        int indice2,
        int tamanhoindice1,
        int tamanhoindice2,
        int alturaCelulaSelecionada,
        int larguraCelulaSelecionada
    )
    {
        for (indice1 = 0; indice1 < tamanhoindice1; indice1++)
        {
            for (indice2 = 0; indice2 < tamanhoindice2; indice2++)
            {
                if (indice1 == alturaCelulaSelecionada && indice2 == larguraCelulaSelecionada)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
                switch (matriz[indice1, indice2])
                {
                    case "|*|":
                        Console.ForegroundColor = ConsoleColor.White;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|1|":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|2|":
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|3|":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|4|":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|5|":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|6|":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|7|":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|8|":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|X|":
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                    case "|_|":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        if (
                            indice1 == alturaCelulaSelecionada
                            && indice2 == larguraCelulaSelecionada
                        )
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(matriz[indice1, indice2]);
                        break;
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
    }
}
