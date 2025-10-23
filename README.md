# Linear Systems & Matrices Solver (Windows Forms App)

A Windows Forms application for solving **linear systems** and **matrices** step by step, using either **Gaussian Elimination** or **Gauss–Jordan Elimination**.  
This project provides a clean, interactive interface with modern classic colors and strong input validation to ensure a smooth, crash-free user experience.

---

## Features

### I. Flexible Input Options
- Users can **enter either a linear system** (in variable form) or **a numerical matrix**.  
- The program intelligently handles missing variables — users **don’t need to input zeros** for missing coefficients.  
- Variables can be any characters (`x`, `y`, `z`, `a`, `b`, `c`, etc.) and **do not need to be entered in order**.  
- Both the system and the matrix can be **modified easily** before solving.

---

### II. Choice of Algorithm
Choose between:
- **Gaussian Elimination**
- **Gauss–Jordan Elimination**

The selected method determines how the system or matrix will be processed and solved.

---

### III. Dynamic & Volatile Input
- Input fields automatically adjust as users enter equations or matrices.  
- No fixed structure — the app dynamically parses input to build the correct augmented matrix representation.  
- Modifying the input updates the underlying data structure without restarting the program.

---

### IV. Robust Error Handling
The program validates every user entry to prevent incorrect or incomplete input.  
It gracefully handles:
- Empty input fields  
- Random non-variable text  
- Numbers only (in a system input)  
- Unsupported special characters  

If invalid input is detected:
- A clear **error message** appears explaining what went wrong.  
- A hint is displayed describing **how to fix the input**.  
- The program **does not crash** or display incorrect results.

---

### V. Step-by-Step Solution Display
For every chosen algorithm:
- The system (if provided as equations) is first **converted into a matrix** automatically.  
- The solving process is displayed **step by step** with textual explanations.  
- Each step shows **what operation is being done** and **the resulting matrix** after that operation.  
- This feature makes the program useful as a **learning tool** for understanding elimination methods.

---

### VI. Clear and Informative Output
After the solving process:
- The program displays:
  - **One unique solution**, or  
  - **No solution**, or  
  - **Infinite solutions**  
- It also explicitly states the **consistency** and **dependency** of the system.

---

### VII. Reset Options
- A **Reset button** allows users to clear both input and output without restarting the program.  
- Two reset modes:
  - **Reset Input Only**
  - **Reset All (Input + Output)**

This ensures quick reusability and a seamless user experience.

---

### VIII. Elegant and Modern Interface
- Built using **Windows Forms**.  
- Designed with a **light modern classic color palette** 
- Intuitive layout with distinct buttons for:
  - Input type selection (System / Matrix)
  - Algorithm selection
  - Solve
  - Reset

---

## Educational Value
This program isn’t just a solver — it’s a **learning tool**.  
Students can use it to:
- Visualize how elimination methods work step-by-step.  
- Understand row operations and system transformations.  
- Practice identifying consistency and dependency in linear systems.

---

## Technical Details
- **Platform:** Windows Forms (.NET Framework / .NET 8)
- **Language:** C#  
- **Algorithms Implemented:** Gaussian Elimination, Gauss–Jordan Elimination
- **Error Handling:** Custom validation and exception-free operation
- **UI Design:** Responsive, modern, educational

---

## How to Run
1. Clone or download the repository:
   ```bash
   git clone https://github.com/your-username/linear-system-solver.git
