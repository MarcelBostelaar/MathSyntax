# MathSyntax

A C# library for dynamically creating mathematical formulas and performing operations with and on them.

## Currently implemented:
- Sums
- Products
- Quotients
- Numeric constants
- Variable constants (constants that can be changed)
- Variables

## Functionality:
- (Partial) derivatives
- Calculations
- Printing of formulas
- Simplification
- Comparison of formulas
- Parallelised versions for all functionality (Using the default Parallel class)

## Notes:
Simplification does not yet do anything beyond removing the following:
- A * 0 -> 0
- A * 1 -> A
- A + 0 -> A
- A / 1 -> A
- 0 / A -> 0
- Number + Number -> Number
- Number * Number -> Number
- Number / Number -> Number

~~Calculating (partial) derivatives from large formulas can take a long time.~~ Lightning fast due to a commit from Daniel.
