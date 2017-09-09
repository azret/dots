 ```csharp
const int INPUTS = 7; const int OUTPUTS = INPUTS;

var X = Vector(INPUTS);

var Y = Dots.create(OUTPUTS);

Dots.path(X, null, Y);

Dots.compute(X, null, Y);

for (int episode = 0; episode< 128 * 1024; episode++)
{
    X = Vector(INPUTS);

    Dots.compute(X, null, Y);

    Dots.learn(Y, 0.1, X);
}

Dots.compute(X, null, Y);
```
