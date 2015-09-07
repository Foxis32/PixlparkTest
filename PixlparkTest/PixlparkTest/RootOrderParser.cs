using System.Collections.Generic;
using PixlparkTest.Model;

namespace PixlparkTest
{
    class RootOrderParser
    {
        public string ApiVersion { get; set; }
        public IEnumerable<Order> Result { get; set; }
    }
}
