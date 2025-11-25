using task_7;

public class Request {
    public int ID {get; set;}
    public int Priority {get; set;}
    public int StepAdd {get; set;}
    public int StepRemove {get; set;}
}

public class RequestComparer : IComparer<Request> {
    public int Compare(Request x, Request y) {
        return y.Priority.CompareTo(x.Priority);
    }
}

class Program {
    static void Main() {
        int N = int.Parse(Console.ReadLine());
        var queue = new MyPriorityQueue<Request>(100, new RequestComparer());
        using StreamWriter logFile = new StreamWriter("/Users/mihailprohorov/Desktop/учеба/C#/task_7/task_7/log.txt");
        int id = 0;
        Request ans = null;
        int maxWaitTime = int.MinValue;
        Random random = new Random();
        for (int step = 1; step <= N; step++) {
            int cnt = random.Next(1, 11);
            for (int i = 1; i <= cnt; i++) {
                Request request = new Request {
                    ID = id++,
                    Priority = random.Next(1, 6),
                    StepAdd = step
                };
                logFile.WriteLine($"ADD {request.ID} {request.Priority} {request.StepAdd}");
                queue.Add(request);
            }
            if (queue.IsEmpty) continue;
            Request rem = queue.Poll();
            rem.StepRemove = step;
            logFile.WriteLine($"REMOVE {rem.ID} {rem.Priority} {rem.StepRemove}");
            if (maxWaitTime < rem.StepRemove - rem.StepAdd) {
                ans = rem; maxWaitTime = rem.StepRemove - rem.StepAdd;
            }
        }

        for (int step = N+1; !queue.IsEmpty; step++) {
            Request rem = queue.Poll();
            rem.StepRemove = step;
            logFile.WriteLine($"REMOVE {rem.ID} {rem.Priority} {rem.StepRemove}");
            if (maxWaitTime < rem.StepRemove - rem.StepAdd) {
                ans = rem; maxWaitTime = rem.StepRemove - rem.StepAdd;
            }
        }
        
        if (ans != null) {
            Console.WriteLine($"Максимальное время ожидания: {maxWaitTime} шагов");
            Console.WriteLine($"Заявка с максимальным временем ожидания:");
            Console.WriteLine($"ID: {ans.ID}, Приоритет: {ans.Priority}");
            Console.WriteLine($"Добавлена на шаге: {ans.StepAdd}");
            Console.WriteLine($"Удалена на шаге: {ans.StepRemove}");
        } else {
            Console.WriteLine("Не было обработано ни одной заявки");
        }
    }
}