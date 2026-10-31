// Regression test for the public API. Verify compares the generated API
// text of the library assembly against a snapshot file (*.verified.txt).
// If an intentional change is made, delete the snapshot file and re-run
// the test (or use the *.received.txt file as the new snapshot).
using NUnit.Framework;
using PublicApiGenerator;
using static VerifyNUnit.Verifier;

namespace Evolutionary.Tests
{
    public class PublicApiTests
    {
        [Test]
        public Task PublicApi_HasNotChanged()
        {
            var assembly = typeof(Population<>).Assembly;

            var api = assembly.GeneratePublicApi(new ApiGeneratorOptions
            {
                // Assembly attributes contain the version and are not part
                // of the public API.
                IncludeAssemblyAttributes = false
            });

            return Verify(api);
        }
    }
}
