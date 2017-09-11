 # Identity Function
 
 ```csharp
const int OUTPUTS = 7;

var Ȳ = Dots.create(OUTPUTS);

for (int episode = 0; episode< 128 * 1024; episode++)
{
    var X̄ = GetInput(Dot.random(), max: OUTPUTS);

    Dots.compute(X̄, null, Ȳ);
    
    Dots.train(Ȳ, null, learningRate: 0.1, learn : X̄);
}

Dots.compute(X̄, null, Ȳ);
```
