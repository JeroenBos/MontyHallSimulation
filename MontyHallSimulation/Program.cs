using System.Diagnostics;

bool simulate(bool stay)
{
    var userChoices = new HashSet<int>() { 1, 2, 3 };
    var hostChoices = new HashSet<int>() { 1, 2, 3 };

    var truth = hostChoices.Draw();
    var userChoice = userChoices.Draw();

    hostChoices.Remove(userChoice);

    var hostChoice = hostChoices.Draw();

    if (!stay) // i.e. switch
    {
        userChoices.Remove(hostChoice);
        if (userChoices.Count != 1) throw new UnreachableException();
        userChoice = userChoices.Draw();
    }
    return userChoice == truth;
}

int total = 1000;
int stayWins = 0;
int switchWins = 0;
for (int i = 0; i < total; i++)
{
    bool stay = (i % 2) == 0;
    bool won = simulate(stay);
    if (won)
    {
        if (stay)
        {
            stayWins++;
        }
        else
        {
            switchWins++;
        }
    }
}

Console.WriteLine($"Out of {total} games, half we switches and half we stayed on our initial choice.");
Console.WriteLine($"- By staying we won {stayWins} times");
Console.WriteLine($"- By switching we won {switchWins} times");
Console.ReadLine();










public static class Extensions
{
    public static T Draw<T>(this HashSet<T> set)
    {
        var result = DrawAndReturn<T>(set);
        set.Remove(result);
        return result;
    }
    public static T DrawAndReturn<T>(this HashSet<T> set)
    {
        var i = Random.Shared.Next(set.Count);
        foreach (var element in set)
        {
            if (i == 0)
            {
                return element;
            }
            i--;
        }
        throw new UnreachableException();
    }
}