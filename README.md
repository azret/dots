 # Identity Function
 
 ```csharp
const int OUTPUTS = 7;

var Ȳ = Dots.create(OUTPUTS);

for (int episode = 0; episode< 128 * 1024; episode++)
{
    var X̄ = GetInput(Dot.random(), max: OUTPUTS);
    
    Dots.train(X̄, Ȳ, null, learn : X̄, rate: 0.1);    
}

Dots.compute(X̄, null, Ȳ);
```
