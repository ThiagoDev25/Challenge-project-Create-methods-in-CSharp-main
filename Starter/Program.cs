using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Posição do jogador no console
int playerX = 0;
int playerY = 0;

// Posição da comida no console
int foodX = 0;
int foodY = 0;

// Strings disponíveis para o jogador e a comida
string[] states = {"('-')", "(^-^)", "(X_X)"};
string[] foods = {"@@@@@", "$$$$$", "#####"};

// String atual do jogador exibida no console
string player = states[0];

// Índice da comida atual
int food = 0;



InitializeGame();
 while (!shouldExit) 
 {
    if (TerminalResized())
        {
        Console.Clear();
        Console.WriteLine("O Console foi redimensionado. Saindo do Programa! ");
        Environment.Exit(0);
        }
    else 
    {
        if (PlayerFeliz())
        {
            Move(2, false);
        }
        else if (PlayerDoente())
        {
            FreezePlayer();
        }
        else 
        {
            Move(otherKeysExit: false);
            
        }
    
    if (Comeu())
    {
        ChangePlayer();
        ShowFood();
    }
    }
 }


// Retorna true se o terminal foi redimensionado
bool TerminalResized() 
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

// Exibe comida aleatória em uma localização aleatória
void ShowFood() 
{
    // Atualiza a comida para um índice aleatório
    food = random.Next(0, foods.Length);

    // Atualiza a posição da comida para uma localização aleatória
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Exibe a comida na localização
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Altera o jogador para corresponder à comida consumida
void ChangePlayer() 
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporariamente impede o jogador de se mover
void FreezePlayer() 
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
}

// Lê a entrada direcional do console e move o jogador
void Move(int speed = 1, bool otherKeysExit = false) 
{
    int lastX = playerX;
    int lastY = playerY;
    
    switch (Console.ReadKey(true).Key) 
    {
        case ConsoleKey.UpArrow:
            playerY--; 
            break;
        case ConsoleKey.DownArrow: 
            playerY++; 
            break;
        case ConsoleKey.LeftArrow:  
            playerX -= speed; 
            break;
        case ConsoleKey.RightArrow: 
            playerX += speed; 
            break;
        case ConsoleKey.Escape:     
            shouldExit = true; 
            break;
        default: 
            shouldExit = otherKeysExit;
            break;
    }

    // Limpa os caracteres na posição anterior
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++) 
    {
        Console.Write(" ");
    }

    // Mantém a posição do jogador dentro dos limites da janela do terminal
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Desenha o jogador na nova localização
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Limpa o console, exibe a comida e o jogador
void InitializeGame() 
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}

/*Método comeu*/
bool Comeu()
{
    return playerX == foodX && playerY == foodY;
}

bool PlayerFeliz()
{
    return player.Equals(states[1]);
}

bool PlayerDoente()
{
    return player.Equals(states[2]);
}