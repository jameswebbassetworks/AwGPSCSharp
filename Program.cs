using CSharpInterviewMessageProcessor;
using System;
using System.IO;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Vehicle Message Processor");
        Console.WriteLine("=========================");
        Console.Write("Enter the path to the message file: ");

        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Error: File path cannot be empty.");
            return;
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File not found at '{filePath}'");
            return;
        }

        var messageQueue = new MessageQueue();
        var cancellationTokenSource = new CancellationTokenSource();

        // Consumer thread - processes messages from queue
        var consumerThread = new Thread(() =>
        {
            var processor = new MessageProcessor();
            try
            {
                while (!messageQueue.IsCompleted)
                {
                    try
                    {
                        var message = messageQueue.Dequeue(cancellationTokenSource.Token);
                        processor.Process(message);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // Queue completed
            }
        });

        consumerThread.Start();

        // Producer - reads file and enqueues messages
        try
        {
            var xmlContent = File.ReadAllText(filePath);
            var parser = new MessageParser();
            var messages = parser.ParseAll(xmlContent);

            foreach (var message in messages)
            {
                messageQueue.Enqueue(message);
            }
            messageQueue.CompleteAdding();

            consumerThread.Join();
            Console.WriteLine($"All {messages.Count} message(s) processed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing messages: {ex.Message}");
            cancellationTokenSource.Cancel();
        }
        finally
        {
            cancellationTokenSource.Dispose();
        }
    }
}