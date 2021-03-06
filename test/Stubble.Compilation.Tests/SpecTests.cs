﻿using Stubble.Compilation.Settings;
using Stubble.Test.Shared.Spec;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Stubble.Compilation.Tests
{
    public class SpecTests
    {
        internal readonly ITestOutputHelper OutputStream;
        internal readonly CompilerSettings Settings;

        public SpecTests(ITestOutputHelper output)
        {
            OutputStream = output;
            Settings = new CompilerSettingsBuilder().BuildSettings();
        }

        [Theory]
        [MemberData(nameof(Specs.SpecTests), MemberType = typeof(Specs))]
        public void CompilationRendererSpecTest(SpecTest data)
        {
            OutputStream.WriteLine(data.Name);

            var stubble = new StubbleCompilationRenderer(Settings);
            var output = data.Partials != null ? stubble.Compile(data.Template, data.Data, data.Partials) : stubble.Compile(data.Template, data.Data);

            var outputResult = output(data.Data);

            OutputStream.WriteLine("Expected \"{0}\", Actual \"{1}\"", data.Expected, outputResult);
            Assert.Equal(data.Expected, outputResult);
        }

        [Theory]
        [MemberData(nameof(Specs.SpecTests), MemberType = typeof(Specs))]
        public async Task CompilationRendererSpecTest_Async(SpecTest data)
        {
            OutputStream.WriteLine(data.Name);

            var stubble = new StubbleCompilationRenderer(Settings);
            var output = await (data.Partials != null ? stubble.CompileAsync(data.Template, data.Data, data.Partials) : stubble.CompileAsync(data.Template, data.Data));

            var outputResult = output(data.Data);

            OutputStream.WriteLine("Expected \"{0}\", Actual \"{1}\"", data.Expected, outputResult);
            Assert.Equal(data.Expected, outputResult);
        }
    }
}
