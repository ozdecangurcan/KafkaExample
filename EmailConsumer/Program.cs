﻿using Confluent.Kafka;
using System;

namespace EmailConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailConsumer();
        }

        public static void EmailConsumer()
        {
            var config = new ConsumerConfig
            {
                GroupId = "emailGroup",
                BootstrapServers = "172.19.208.1:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore,string>(config).Build())
            {
                consumer.Subscribe("emailTopic");

                try
                {
                    while (true)
                    {
                        var result = consumer.Consume();
                        Console.WriteLine($"Consumed Message: '{result.Message.Value}' Topic: '{result.TopicPartitionOffset}'");
                    }
                }
                catch (ConsumeException ex)
                {

                    Console.WriteLine(ex.Error.Reason);
                }
            }
        }

        
    }
}
