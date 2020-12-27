``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1256 (1909/November2018Update/19H2)
Intel Core i5-6200U CPU 2.30GHz (Skylake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|         Method |        Mean |     Error |    StdDev |      Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |------------:|----------:|----------:|------------:|-------:|------:|------:|----------:|
|     StaticCall |   0.0386 ns | 0.0376 ns | 0.0475 ns |   0.0088 ns |      - |     - |     - |         - |
|   InstanceCall |   0.0688 ns | 0.0455 ns | 0.0785 ns |   0.0430 ns |      - |     - |     - |         - |
|    VirtualCall |   1.1274 ns | 0.0199 ns | 0.0177 ns |   1.1293 ns |      - |     - |     - |         - |
|    GenericCall |   0.3796 ns | 0.0212 ns | 0.0177 ns |   0.3823 ns |      - |     - |     - |         - |
|    DynamicCall |  18.5987 ns | 0.3718 ns | 0.7760 ns |  18.5300 ns | 0.0153 |     - |     - |      24 B |
| ReflectionCall | 154.6327 ns | 2.3046 ns | 2.1558 ns | 154.6983 ns | 0.0305 |     - |     - |      48 B |
