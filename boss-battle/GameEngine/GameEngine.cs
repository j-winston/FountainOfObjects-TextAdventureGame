// Game Engine 
// Responsibilites: Orchestrating the whole thing
//
//
using System;
namespace BossBattle.Core;

public class GameEngine
{
    private World? _world;
    private Player? _player;
    private bool _isRunning;


    public void Initialize()
    {
        _world = WorldFactory.GenerateWorld(5, 5);
        _player = new Player();
        _isRunning = true;

    }

    public void Run()
    {
        while (_isRunning)
        {
            Render();

            var input = GetInput();

            ProcessInput(input);

            UpdateState();


        }


    }

    // Get current room and display on screen 
    public void Render()
    {
        if (_world is not null && _player is not null)
        {
            IRoom? currentRoom = GetCurrentRoom();

            if (currentRoom is not null)
            {

                //TODO: prettify the title of the room 
                Console.WriteLine($"~~~{currentRoom?.RoomName}~~~\n");
                Console.WriteLine(currentRoom?.Description);

            }
            else
            {
                Console.WriteLine("You've hit a wall");
            }


        }

    }

    public IRoom? GetCurrentRoom()
    {
        if (_world is not null && _player is not null)
        {
            return _world.GetRoomAt(_player.X, _player.Y);
        }

        return null;
    }

    public string GetInput()
    {
        Console.WriteLine("What do you want to do?");

        string? input = Console.ReadLine();

        if (input is not null)
        {
            return input;
        }

        return String.Empty;

    }

    public bool isValidCommand(string input)
    {
        return input.Split(" ").GetLength(0) > 1;
    }

    // This can later be modified Maniac Mansion style 
    public void ProcessInput(string input)
    {

        switch (input)
        {
            case "move west":
                {
                    // TODO: Start here check if movement is within bounds 
                    _player?.MoveWest();
                    break;

                }
            case "move east":
                _player?.MoveEast();
                break;


            default: break;

        }


    }

    public void UpdateState()
    {

    }




}


