# A-_Pathfinding_with_grid
Contains all scripts required to build a basic A* pathfinding algorithm. Non-optimized.
Works by checking all neighbouring nodes including diagonals.

How to use :
- Create an empty gameObject for the grid and assign it the grid script
  - Enter grid dimensions, cell-size, start position, obstacle mask and distance between cells
- Create an empty gameobject for the pathfinding and assign it pathfinding script
  - Add reference to grid script, start position, end position


To do : 
- Add selector to chose 4 neighbouring nodes (Up/Down/Left/Right) Or 8 neighbouring nodes (With diagonals)
