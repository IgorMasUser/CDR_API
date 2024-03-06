using CDR_API.Models;

namespace CDR_API_tests
{
    public class MockCallRecords
    {
        public List<CallRecord> callRecords = new List<CallRecord>
        {
               new CallRecord
                {
                    CallerId = "441215598896",
                    Recipient = "448000480968",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:21:33"),
                    Duration = 43,
                    Cost = -0.006M,
                    Reference = "C5DA9724701EEBBA95CA2CC5617BA93E4",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "442036401149",
                    Recipient = "44800833833",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:00:47"),
                    Duration = 244,
                    Cost = 0,
                    Reference = "C50B5A7BDB8D68B8512BB14A9D363CAA1",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "443330132430",
                    Recipient = "448003580672",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:19:50"),
                    Duration = 13,
                    Cost = 0,
                    Reference = "CF3154CFA82653893CA1D77A6627C94B5",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "442035515300",
                    Recipient = "448006783320",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:33:32"),
                    Duration = 64,
                    Cost = 0,
                    Reference = "CE9BABA57E4CA258BCF66B8FC2E206965",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "443330132430",
                    Recipient = "448000480968",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:21:50"),
                    Duration = 31,
                    Cost = 0,
                    Reference = "C0FAAB1E6424B20D1625FEAAD5936053E",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "443330132430",
                    Recipient = "448001830044",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:32:40"),
                    Duration = 373,
                    Cost = 0,
                    Reference = "C639033F0752A937D951A6A2E33EB6910",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "441760723301",
                    Recipient = "448000480968",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:33:37"),
                    Duration = 2428,
                    Cost = 0,
                    Reference = "C448A51ECB014E5B2D9D96620E2F2397E",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "442072657300",
                    Recipient = "448088004444",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:05:29"),
                    Duration = 149,
                    Cost = 0,
                    Reference = "C6C4EC9A8C4847E8AD1B1D6CD02491E79",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "443330132430",
                    Recipient = "448004960493",
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:32:48"),
                    Duration = 693,
                    Cost = 0,
                    Reference = "C2BF812F9B32CD37164AB07C69A36111D",
                    Currency = "GBP"
                },
                new CallRecord
                {
                    CallerId = "443330132431", 
                    Recipient = "448003580673", 
                    CallDate = new DateTime(2016, 8, 16),
                    EndTime = TimeSpan.Parse("14:19:51"),
                    Duration = 693,
                    Cost = 0,
                    Reference = "C2BF812F9B32CD37164AB07C69A36111D",
                    Currency = "GBP"
                  }

        };
    }
}
