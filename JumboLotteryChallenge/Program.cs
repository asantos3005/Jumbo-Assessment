using System.Globalization;
using System.Linq;

// Data
HashSet<int> John1 = new HashSet<int> {7, 9, 13, 24, 33, 40};
HashSet<int> John2 = new HashSet<int> {16, 19, 22, 29, 31, 39};
HashSet<int> John3 = new HashSet<int> {1, 7, 18, 22, 30, 36};

HashSet<int> Mary1 = new HashSet<int> {2, 22, 13, 24, 32, 39};
HashSet<int> Mary2 = new HashSet<int> {7, 22, 24, 31, 33, 40};
HashSet<int> Mary3 = new HashSet<int> {3, 7, 18, 21, 37, 38};

HashSet<int> Jack1 = new HashSet<int> {3, 5, 7, 11, 22, 24, 31, 34, 40};


HashSet<int> winning = new HashSet<int> {7, 22, 24, 31, 33, 40};

/// Lottery Tickets
/// 

LotterySet winningDraw = new LotterySet(winning);
LotteryTicket JohnTicket1 = new LotteryTicket(John1, "John");
LotteryTicket JohnTicket2 = new LotteryTicket(John2, "John");
LotteryTicket JohnTicket3 = new LotteryTicket(John3, "John");
LotteryTicket MaryTicket1 = new LotteryTicket(Mary1, "Mary");
LotteryTicket MaryTicket2 = new LotteryTicket(Mary2, "Mary");
LotteryTicket MaryTicket3 = new LotteryTicket(Mary3, "Mary");



LotteryTicket JackTicket1 = new LotteryTicket(Jack1, "Jack");

// BONUS OBJECTIVE - didn't finish in enough time
//LotteryWinChecker.scoreTicketMoreThanSixNumbers(JackTicket1);


LotteryTicket[] tickets = new LotteryTicket[]{ JohnTicket1, JohnTicket2, JohnTicket3, MaryTicket1, MaryTicket2, MaryTicket3};


foreach(LotteryTicket ticket in tickets)
{
    LotteryWinChecker.scoreTickets(ticket, winningDraw);
    Console.WriteLine(" ");
}



public enum PrizeDivision
{
    Division1,
    Division2,
    Division3,
    Division4,
    NoPrize
    
}


// Lottery Set base class because Lottery Results aren't associated with owner and don't need name.
public class LotterySet
{
    public HashSet<int> Numbers { get; set; }

    public LotterySet(HashSet<int> numbers)
    {
        // For assumptions sake, System Ticket Entry can go up to 10 numbers
        if (numbers.Count < 6 || numbers.Count > 10)
        { 
            throw new ArgumentException("A lottery set must be 6 and 10 numbers.");
        }

        if (numbers.Any(n => n < 1 || n > 40))
        {
            throw new ArgumentException("Lottery numbers must be between 1 and 40");
        }
        Numbers = new HashSet<int>(numbers);
    }
}

public class LotteryTicket : LotterySet
{
    public string OwnerName { get; set; }
    // public HashSet<int> Numbers { get; set; }

    public LotteryTicket(HashSet<int> numbers, string name) : base(numbers)
    {
        // For assumptions sake, System Ticket Entry can go up to 10 numbers
        if (numbers.Count < 6 && numbers.Count > 10)
        { 
            throw new ArgumentException("A lottery set must contain at least 6 numbers.");
        }

        if (numbers.Any(n => n < 1 || n > 40))
        {
            throw new ArgumentException("Lottery numbers must be between 1 and 40");
        }
        Numbers = new HashSet<int>(numbers);
        OwnerName = name;
    }
}

public static class LotteryWinChecker
{
    public static void scoreTickets(LotteryTicket ticket, LotterySet winningNumbers)
    {
        HashSet<int> matchingNumbers = CheckMatchingNumbers(ticket, winningNumbers);
        PrizeDivision prize = determinePrizeDivision(matchingNumbers.Count());
        outputResultMessage(matchingNumbers, prize, ticket.OwnerName, winningNumbers);
    }

    // return matching numbers only - original case
    public static HashSet<int> CheckMatchingNumbers(LotteryTicket ticket, LotterySet winningNumbers)
    {
       var matchingNumbers = ticket.Numbers.Intersect(winningNumbers.Numbers).ToHashSet();
       //testing
       //Console.WriteLine(matchingNumbers);
       return matchingNumbers;
    }

    // BONUS FUNCTION
    // Didn't finish in time
    /*
    public static void scoreTicketMoreThanSixNumbers(LotteryTicket ticket)
    {
        int[] numbers = ticket.Numbers.ToArray();
        HashSet<int> numbersAlreadyTried = new HashSet<int>();
        HashSet<int> uniqueCombinationToTry = new HashSet<int>();
        for(int i = 0; i < numbers.Count(); i++)
            {
                foreach(int number in ticket.Numbers)
                {
                    if (!numbersAlreadyTried.Contains(number))
                    {
                        uniqueCombinationToTry.Add(number);
                    }
                    if (uniqueCombinationToTry.Count == 6)
                    {
                        Console.WriteLine(formatHashSetToString(uniqueCombinationToTry));
                        uniqueCombinationToTry.Clear();
                        numbersAlreadyTried.Add(numbers[i]);
                        break;
                    }
                }
            }
    }
    */
    
    
    public static PrizeDivision determinePrizeDivision(int matchingNumbersCount)
    {
        switch (matchingNumbersCount)
        {
            case 6:
                return PrizeDivision.Division1;
            case 5: 
                return PrizeDivision.Division2;
            case 4: 
                return PrizeDivision.Division3;
            case 3: 
                return PrizeDivision.Division4;
            default:
                return PrizeDivision.NoPrize;
        }
    }

    public static void outputResultMessage(HashSet<int> matchingNumbers, PrizeDivision prize, string name, LotterySet winningDraw)
    {
        Console.WriteLine($"{name} wins {prize} with matches {formatHashSetToString(matchingNumbers)} for game {formatHashSetToString(winningDraw.Numbers)}");
    }

    public static string formatHashSetToString(HashSet<int> toFormat)
    {
        string formattedString = string.Join(", ", toFormat);
        return formattedString;
    }

    
   
}


