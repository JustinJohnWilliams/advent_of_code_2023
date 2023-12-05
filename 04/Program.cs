var sw = new System.Diagnostics.Stopwatch();
sw.Start();

Console.WriteLine($"*************Day 4 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("example.txt");

sw.Stop();

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"Time (total)\t\t: {sw.Elapsed.TotalMilliseconds}ms");
Console.WriteLine($"*************Day 4 START*************");

(int result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var winning_numbers = new Dictionary<int, List<int>>();
    var chosen_numbers = new Dictionary<int, List<int>>();

    foreach(var line in lines)
    {
        var id_and_number_parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
        var id = Convert.ToInt32(id_and_number_parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
        var winners = id_and_number_parts[1].Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        winning_numbers.Add(id, winners.Select(c => Convert.ToInt32(c)).ToList());

        var picks = id_and_number_parts[1].Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        chosen_numbers.Add(id, picks.Select(c => Convert.ToInt32(c)).ToList());
    }

    var winning_hands = find_winners_in_games(winning_numbers, chosen_numbers);

    var total = 0;

    foreach(var winner in winning_hands.Where(c => c.Value.Any()))
    {
        total += Convert.ToInt32(Math.Pow(2, winner.Value.Count() - 1));
    }

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

(int result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var lines = File.ReadAllLines(file);
    var winning_numbers = new Dictionary<int, List<int>>();
    var chosen_numbers = new Dictionary<int, List<int>>();

    var game_count = lines.Length;

    foreach(var line in lines)
    {
        var id_and_number_parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
        var id = Convert.ToInt32(id_and_number_parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
        var winners = id_and_number_parts[1].Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        winning_numbers.Add(id, winners.Select(c => Convert.ToInt32(c)).ToList());

        var picks = id_and_number_parts[1].Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        chosen_numbers.Add(id, picks.Select(c => Convert.ToInt32(c)).ToList());
    }

    var winning_hands = find_winners_in_games(winning_numbers, chosen_numbers);
    var prize_cards = new List<KeyValuePair<int, List<int>>>();

    var total = 0;

    foreach(var winner in winning_hands)
    {
        // Add original card
        prize_cards.Add(new KeyValuePair<int, List<int>>(winner.Key, winner.Value));

        // get the new game cards
        var new_card_count = winner.Key + winner.Value.Count > game_count ? (game_count - winner.Key) : winner.Value.Count;
        //Console.WriteLine($"Card {winner.Key} has {new_card_count} matching numbers");

        for(int i = 1; i <= new_card_count; i++)
        {
            //Console.WriteLine($"Adding {winner.Key + i} to {winner.Key}");
            prize_cards.Add(new KeyValuePair<int, List<int>>(winner.Key + i, winning_numbers[winner.Key + i]));
        }
    }

    //foreach(var pc in prize_cards.OrderBy(c => c.Key))
    //{
    //   Console.WriteLine($"{pc.Key}: {string.Join(",", pc.Value)}");
    //}

    //Console.WriteLine($"Total is: {prize_cards.Count}");

    sw.Stop();

    return (total, sw.Elapsed.TotalMilliseconds);
}

List<KeyValuePair<int, List<int>>> find_winners_in_games(Dictionary<int, List<int>> winning_numbers, Dictionary<int, List<int>> chosen_numbers)
{
    var results = new List<KeyValuePair<int, List<int>>>();

    foreach(var key in winning_numbers.Keys)
    {
        if(chosen_numbers.ContainsKey(key))
        {
            results.Add(new KeyValuePair<int, List<int>>(key, winning_numbers[key].Intersect(chosen_numbers[key]).ToList()));
        }
    }

    return results;
}
