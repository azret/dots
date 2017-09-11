 # Identity Function
 
 ```csharp
const int INPUTS = 7; const int OUTPUTS = INPUTS;

var X̄ = GetInputVector(INPUTS);

// Create an output layer

var Ȳ = Dots.create(OUTPUTS);

// Draw a path from X to Y

Dots.path(X, null, Y);

// Learn the ȳ = f(x̄) = x̄ function

for (int episode = 0; episode< 128 * 1024; episode++)
{
    X = GetInputVector(INPUTS);

    Dots.compute(X̄, null, Ȳ);

    Dots.train(Ȳ, 0.1, learn : X̄);
}

Dots.compute(X̄, null, Ȳ);
```
