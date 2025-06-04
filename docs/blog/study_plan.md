# Mathematics Study Guide

## Overview

- **Goal**: Build a mathematical foundation for GNC algorithm development, trajectory optimization, and uncertainty quantification in space missions.
- **Starting Point**: Comfortable with precalculus, self-studying calculus (assumed early Calculus I), little linear algebra, no real/functional analysis.
- **Timeline**: ~2.5–3 years (10–15 hours/week, self-study), adjustable based on progress.
- **Structure**: Five phases, from foundational to specialized topics, with a dependency chart, detailed subtopics, relevance, resources, and study times.

## Dependency Chart

The following table outlines the prerequisites for each topic and how they connect to subsequent topics, guiding your study order.

| **Topic**                              | **Prerequisites**                                                          | **Leads To**                                                         | **Notes**                                                                |
| -------------------------------------- | -------------------------------------------------------------------------- | -------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| Precalculus                            | None                                                                       | Calculus I                                                           | Foundation for all mathematics; assumed mastered.                        |
| Calculus I                             | Precalculus                                                                | Calculus II, Linear Algebra, ODEs                                    | Focus on derivatives for dynamics modeling.                              |
| Calculus II                            | Calculus I                                                                 | Calculus III, ODEs, Probability and Statistics                       | Integrals and series for energy calculations and approximations.         |
| Calculus III                           | Calculus II                                                                | PDEs, Dynamical Systems, Orbital Mechanics                           | Multivariable calculus for 3D spacecraft motion.                         |
| Linear Algebra                         | Calculus I (basic)                                                         | ODEs, Control Theory, Numerical Analysis, Real Analysis              | Critical for state-space models and GNC algorithms.                      |
| Ordinary Differential Equations (ODEs) | Calculus I, Linear Algebra (basic)                                         | PDEs, Dynamical Systems, Control Theory, Numerical Analysis          | Models spacecraft dynamics; numerical methods for simulations.           |
| Probability and Statistics             | Calculus II                                                                | Stochastic Processes, Estimation and Filtering                       | Essential for uncertainty modeling in GNC.                               |
| Numerical Analysis                     | Calculus I, Linear Algebra, ODEs (basic)                                   | PDEs, Optimization, Functional Analysis                              | Numerical methods for real-time GNC algorithms.                          |
| Partial Differential Equations (PDEs)  | Calculus III, ODEs, Linear Algebra                                         | Functional Analysis, Dynamical Systems                               | Models environmental dynamics (e.g., Mars atmosphere, asteroid gravity). |
| Classical Control Theory               | Linear Algebra, ODEs                                                       | Advanced Control Theory, Estimation and Filtering, Attitude Dynamics | Foundation for GNC system design.                                        |
| Dynamical Systems                      | ODEs, Calculus III, Linear Algebra                                         | Advanced Control Theory, Orbital Mechanics, Functional Analysis      | Analyzes spacecraft and asteroid dynamics.                               |
| Optimization                           | Calculus I, Linear Algebra, Calculus III                                   | Advanced Control Theory, Orbital Mechanics                           | Optimizes trajectories and control strategies.                           |
| Real Analysis                          | Calculus II, Linear Algebra (basic)                                        | Functional Analysis, Stochastic Processes                            | Rigorous foundation for advanced mathematics.                            |
| Functional Analysis                    | Real Analysis, Linear Algebra                                              | Stochastic Processes, PDEs (advanced), Advanced Control Theory       | Critical for GNC algorithms, PDE solvers, and stability analysis.        |
| Stochastic Processes                   | Probability and Statistics, Real Analysis                                  | Estimation and Filtering, Functional Analysis (advanced)             | Models uncertainties in navigation and environmental conditions.         |
| Advanced Control Theory                | Classical Control Theory, Dynamical Systems, Optimization                  | Attitude Dynamics, Estimation and Filtering, Orbital Mechanics       | Designs robust GNC systems for complex missions.                         |
| Orbital Mechanics                      | Calculus III, ODEs, Dynamical Systems                                      | Attitude Dynamics, Estimation and Filtering                          | Designs trajectories for Mars missions and refueling.                    |
| Attitude Dynamics and Control          | Classical Control Theory, Dynamical Systems, Linear Algebra                | Estimation and Filtering                                             | Ensures spacecraft orientation for navigation.                           |
| Estimation and Filtering               | Probability and Statistics, Classical Control Theory, Stochastic Processes | None (capstone for GNC)                                              | Provides accurate navigation for Mars rovers and orbital spacecraft.     |

## Phase 1: Foundational Mathematics (Calculus and Early Undergraduate)

Focus on completing calculus and building linear algebra skills, given your current self-study.

### 1. Calculus I (Single-Variable Calculus: Limits and Derivatives)

- **Subtopics**:
  - **Limits**: Definition, one-sided limits, limits at infinity, continuity, epsilon-delta definition.
  - **Derivatives**: Definition, power/product/quotient/chain rules, implicit differentiation, higher-order derivatives.
  - **Applications**: Optimization (max/min), related rates, tangent lines, curve sketching, L’Hôpital’s rule.
  - **Exponential and Logarithmic Functions**: Derivatives, applications to growth/decay.
- **Relevance**:
  - Derivatives model spacecraft velocity/acceleration (e.g., Mars EDL).
  - Optimization minimizes fuel in trajectory design.
  - Limits ensure numerical stability in GNC algorithms.
- **Study Time**: 4–6 weeks (intensive, since you’re self-studying)
- **Resources**:
  - _Calculus: Early Transcendentals_ by James Stewart (standard, clear examples)
  - _Calculus_ by Michael Spivak (rigorous, builds intuition)
  - _Calculus Made Easy_ by Silvanus P. Thompson (intuitive, beginner-friendly)
  - MIT OpenCourseWare: Single Variable Calculus (18.01, lectures by David Jerison)
  - Paul’s Online Math Notes (free notes, practice problems)
  - YouTube: 3Blue1Brown’s Essence of Calculus
- **Practice**: Optimize a rocket’s thrust profile, compute derivatives for orbital velocity.
- **Note**: Since you’re self-studying, focus on mastering derivatives and optimization; use Khan Academy for extra practice.

### 2. Calculus II (Single-Variable Calculus: Integrals)

- **Subtopics**:
  - **Integrals**: Riemann sums, definite/indefinite integrals, Fundamental Theorem of Calculus.
  - **Techniques**: Substitution, integration by parts, trigonometric integrals/substitutions, partial fractions.
  - **Applications**: Areas, volumes, arc length, work, center of mass, probability density.
  - **Improper Integrals**: Convergence, applications to infinite domains.
  - **Sequences and Series**: Convergence tests (ratio, root), power series, Taylor/Maclaurin series.
- **Relevance**:
  - Integrals compute propellant mass or energy in spacecraft design.
  - Taylor series approximate nonlinear dynamics in GNC.
  - Improper integrals model uncertainty distributions (e.g., Mars environmental factors).
- **Study Time**: 4–6 weeks
- **Resources**:
  - _Calculus: Early Transcendentals_ by Stewart
  - _Calculus_ by Thomas and Finney (applied, engineering-focused)
  - _A First Course in Calculus_ by Serge Lang (concise, rigorous)
  - Khan Academy Calculus 2 (free videos, exercises)
  - YouTube: PatrickJMT’s Calculus Videos
- **Practice**: Compute propellant for a Mars transfer, approximate perturbations with Taylor series.

### 3. Calculus III (Multivariable Calculus)

- **Subtopics**:
  - **Vectors and Geometry**: Vector operations, lines, planes, parametric surfaces.
  - **Partial Derivatives**: Chain rule, directional derivatives, gradient, Hessian, Taylor expansions.
  - **Multiple Integrals**: Double/triple integrals, polar/cylindrical/spherical coordinates, change of variables.
  - **Vector Calculus**: Line/surface integrals, gradient, divergence, curl, Green’s/Stokes’/Divergence theorems.
  - **Applications**: Mass, moments of inertia, flux, work in gravitational fields.
- **Relevance**:
  - Models 3D spacecraft motion and gravitational fields (e.g., asteroids).
  - Vector calculus analyzes electromagnetic fields in navigation sensors.
  - Multiple integrals compute spacecraft moments for attitude control.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Calculus: Early Transcendentals_ by Stewart (Multivariable section)
  - _Multivariable Calculus_ by Jerrold E. Marsden and Anthony J. Tromba (applied, concise)
  - _Vector Calculus_ by Susan J. Colley (focused, clear)
  - MIT OpenCourseWare: Multivariable Calculus (18.02, lectures by Denis Auroux)
  - YouTube: Trefor Bazett’s Vector Calculus Playlist
- **Practice**: Compute gravitational potential for an asteroid, analyze atmospheric flow during Mars EDL.

### 4. Linear Algebra

- **Subtopics**:
  - **Systems of Equations**: Gaussian elimination, row reduction, matrix representations.
  - **Matrix Algebra**: Matrix operations, determinants, inverses, eigenvalues/eigenvectors, diagonalization.
  - **Vector Spaces**: Subspaces, basis, dimension, linear independence, linear transformations.
  - **Inner Products**: Orthogonality, Gram-Schmidt process, projections, least squares.
  - **Advanced Topics**: Singular value decomposition (SVD), QR decomposition, Jordan form.
- **Relevance**:
  - Underpins state-space models in GNC (e.g., Kalman filters for Mars rovers).
  - Eigenvalues analyze stability in control systems.
  - SVD processes sensor data for orbital refueling.
- **Study Time**: 6–8 weeks (extra time due to limited prior knowledge)
- **Resources**:
  - _Linear Algebra and Its Applications_ by David C. Lay (applied, beginner-friendly)
  - _Introduction to Linear Algebra_ by Gilbert Strang (intuitive, with free MIT lectures)
  - _Linear Algebra Done Right_ by Sheldon Axler (theoretical, for rigor)
  - _Linear Algebra: Step by Step_ by Kuldeep Singh (self-study, detailed)
  - MIT OpenCourseWare: Linear Algebra (18.06, by Gilbert Strang)
  - YouTube: 3Blue1Brown’s Essence of Linear Algebra
- **Practice**: Solve attitude dynamics using quaternions, compute eigenvalues for a control system.
- **Note**: Start with basic matrices and systems; Strang’s lectures are ideal for beginners.

## Phase 2: Core Undergraduate Mathematics for Aerospace and GNC

Build on calculus and linear algebra to model dynamic systems and control algorithms.

### 5. Ordinary Differential Equations (ODEs)

- **Subtopics**:
  - **First-Order ODEs**: Separable equations, exact equations, integrating factors, linear equations.
  - **Second-Order ODEs**: Homogeneous/nonhomogeneous, variation of parameters, reduction of order.
  - **Systems of ODEs**: Matrix methods, eigenvalues, phase plane analysis.
  - **Laplace Transforms**: Solving ODEs, step/impulse functions, convolution theorem.
  - **Numerical Methods**: Euler’s method, Runge-Kutta methods, stability analysis.
- **Relevance**:
  - Models orbital dynamics, attitude control, and rover motion.
  - Systems of ODEs describe multi-body interactions in orbital refueling.
  - Numerical methods simulate GNC algorithms.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _A First Course in Differential Equations with Modeling Applications_ by Dennis G. Zill
  - _Elementary Differential Equations and Boundary Value Problems_ by Boyce and DiPrima
  - _Ordinary Differential Equations_ by Morris Tenenbaum and Harry Pollard
  - MIT OpenCourseWare: Differential Equations (18.03)
  - YouTube: Steve Brunton’s Control Bootcamp (ODEs in control)
- **Practice**: Model a low-thrust Mars trajectory, simulate a rover’s suspension.

### 6. Probability and Statistics

- **Subtopics**:
  - **Probability**: Axioms, conditional probability, independence, Bayes’ theorem, law of total probability.
  - **Random Variables**: Discrete/continuous, expectation, variance, covariance, moment-generating functions.
  - **Distributions**: Binomial, Poisson, normal, exponential, chi-squared, t-distribution.
  - **Statistical Inference**: Hypothesis testing, confidence intervals, p-values, maximum likelihood estimation.
  - **Stochastic Processes**: Markov chains, Poisson processes, introduction to Brownian motion.
- **Relevance**:
  - Models sensor noise and environmental uncertainties (e.g., Mars dust storms).
  - Stochastic processes underpin Kalman filtering for navigation.
  - Statistical inference quantifies risks in asteroid mining.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Introduction to Probability_ by Joseph K. Blitzstein and Jessica Hwang (intuitive)
  - _A First Course in Probability_ by Sheldon Ross (clear, applied)
  - _Probability and Statistics_ by Morris H. DeGroot and Mark J. Schervish
  - MIT OpenCourseWare: Probability and Statistics (18.05)
  - YouTube: StatQuest with Josh Starmer
- **Practice**: Model sensor noise for a Mars rover, compute failure probabilities for refueling.

### 7. Numerical Analysis

- **Subtopics**:
  - **Numerical Linear Algebra**: Gaussian elimination, LU decomposition, iterative methods (Jacobi, Gauss-Seidel).
  - **Interpolation**: Lagrange polynomials, Newton interpolation, splines.
  - **Numerical Integration**: Trapezoidal rule, Simpson’s rule, Gaussian quadrature.
  - **Numerical ODEs**: Euler, Runge-Kutta, multistep methods, stiff equations.
  - **Root-Finding**: Bisection, Newton-Raphson, secant method.
  - **Error Analysis**: Round-off errors, truncation errors, condition numbers.
- **Relevance**:
  - Simulates spacecraft trajectories and solves PDEs for environmental models.
  - Error analysis ensures reliability in GNC algorithms.
  - Interpolation processes navigation data in real time.
- **Study Time**: 4–6 weeks
- **Resources**:
  - _Numerical Analysis_ by Richard L. Burden and J. Douglas Faires
  - _Numerical Methods for Engineers_ by Steven C. Chapra and Raymond P. Canale
  - _Scientific Computing: An Introductory Survey_ by Michael T. Heath
  - MIT OpenCourseWare: Numerical Analysis (18.330)
  - Cleve Moler’s _Numerical Computing with MATLAB_ (free online)
- **Practice**: Implement Runge-Kutta for orbital simulation, analyze error in a GNC algorithm.

## Phase 3: Advanced Undergraduate/Graduate-Level Mathematics for GNC

Introduce specialized topics for dynamic systems and control, preparing for analysis.

### 8. Partial Differential Equations (PDEs)

- **Subtopics**:
  - **Classification**: Elliptic, parabolic, hyperbolic PDEs, characteristics.
  - **Analytical Methods**: Separation of variables, Fourier series, eigenfunction expansions.
  - **Numerical Methods**: Finite difference, finite element, finite volume methods.
  - **Green’s Functions**: Solutions to boundary value problems, integral representations.
  - **Weak Solutions**: Introduction to Sobolev spaces, variational methods.
- **Relevance**:
  - Models gravitational fields (asteroids), atmospheric dynamics (Mars EDL), and thermal control.
  - Finite element methods simulate spacecraft structures.
  - Sobolev spaces solve irregular PDEs for GNC.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Partial Differential Equations_ by Lawrence C. Evans (graduate-level)
  - _Introduction to Partial Differential Equations_ by Walter A. Strauss (accessible)
  - _Numerical Solution of Partial Differential Equations_ by G.D. Smith
  - MIT OpenCourseWare: Partial Differential Equations (18.336)
  - YouTube: Dr. Chris Tisdell’s PDE Videos
- **Practice**: Solve a heat transfer PDE for a Mars lander, implement finite difference method.

### 9. Classical Control Theory

- **Subtopics**:
  - **Transfer Functions**: Laplace transforms, poles, zeros, impulse response.
  - **System Modeling**: Block diagrams, feedback loops, open/closed-loop systems.
  - **Stability**: Routh-Hurwitz criterion, Nyquist stability, root locus.
  - **Controllers**: PID control, lead-lag compensators, gain scheduling.
  - **Frequency Response**: Bode plots, gain/phase margins, frequency-domain design.
- **Relevance**:
  - Designs stable GNC systems for spacecraft attitude and rover navigation.
  - PID controllers are standard in aerospace for real-time control.
  - Stability analysis ensures robustness in dynamic environments.
- **Study Time**: 4–6 weeks
- **Resources**:
  - _Modern Control Engineering_ by Katsuhiko Ogata
  - _Feedback Control of Dynamic Systems_ by Gene F. Franklin, J. David Powell, and Abbas Emami-Naeini
  - _Control Systems Engineering_ by Norman S. Nise
  - MIT OpenCourseWare: Feedback Control Systems (16.31)
  - YouTube: Brian Douglas’s Control Systems Lectures
- **Practice**: Design a PID controller for a spacecraft thruster, analyze stability for a rover.

### 10. Dynamical Systems

- **Subtopics**:
  - **Linear Systems**: Equilibrium points, phase plane analysis, stability.
  - **Nonlinear Systems**: Lyapunov functions, limit cycles, Poincaré maps.
  - **Bifurcations**: Saddle-node, Hopf, period-doubling bifurcations.
  - **Chaos**: Strange attractors, Lyapunov exponents, fractal dimensions.
  - **Hamiltonian Systems**: Energy-based modeling, applications to orbits.
- **Relevance**:
  - Models spacecraft orbits, asteroid spin, and multi-agent interactions.
  - Lyapunov methods ensure stability in GNC algorithms.
  - Chaos analysis is relevant for asteroid navigation.
- **Streamlined Study**: Since you’re new to analysis, focus on linear systems and Lyapunov stability.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Nonlinear Dynamics and Chaos_ by Steven H. Strogatz (accessible)
  - _Differential Equations, Dynamical Systems, and an Introduction to Chaos_ by Hirsch, Smale, and Devaney
  - _Introduction to Applied Nonlinear Dynamical Systems and Chaos_ by Stephen Wiggins
  - MIT OpenCourseWare: Nonlinear Dynamics (18.353)
  - YouTube: Steve Brunton’s Data-Driven Dynamical Systems
- **Practice**: Analyze stability of an orbital rendezvous, model asteroid dynamics.

### 11. Optimization

- **Subtopics**:
  - **Linear Programming**: Simplex method, duality, sensitivity analysis.
  - **Nonlinear Optimization**: Gradient descent, Newton’s method, conjugate gradient.
  - **Convex Optimization**: Convex sets, functions, Lagrange duality, interior-point methods.
  - **Constrained Optimization**: Lagrange multipliers, Karush-Kuhn-Tucker (KKT) conditions.
  - **Optimal Control**: Pontryagin’s maximum principle, Bellman’s principle.
- **Relevance**:
  - Minimizes fuel in Mars transfers and orbital refueling.
  - Optimal control designs efficient GNC strategies.
  - Convex optimization solves scheduling for orbital logistics.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Convex Optimization_ by Stephen Boyd and Lieven Vandenberghe (free online)
  - _Nonlinear Programming_ by Dimitri P. Bertsekas
  - _Optimal Control Theory: An Introduction_ by Donald E. Kirk
  - MIT OpenCourseWare: Optimization Methods (15.093)
  - Stanford Online: Convex Optimization (CVX101)
- **Practice**: Optimize a low-thrust trajectory to Mars, solve a refueling scheduling problem.

## Phase 4: Graduate-Level Mathematics for Functional Analysis and GNC

Introduce real and functional analysis, critical for advanced GNC and your goals.

### 12. Real Analysis

- **Subtopics**:
  - **Real Numbers**: Completeness, sequences, series, convergence tests.
  - **Functions**: Continuity, differentiability, uniform convergence, Weierstrass theorem.
  - **Integration**: Riemann integral, Lebesgue measure, Lebesgue integral, dominated convergence.
  - **Metric Spaces**: Open/closed sets, compactness, completeness, Banach fixed-point theorem.
  - **Sequences of Functions**: Pointwise vs. uniform convergence, Arzelà-Ascoli theorem.
- **Relevance**:
  - Provides rigor for functional analysis, ensuring precise convergence in GNC algorithms.
  - Lebesgue integration is foundational for L^p-spaces in signal processing and PDEs.
  - Metric spaces underpin numerical analysis for trajectory optimization.
- **Streamlined Study**: Focus on Riemann integration and metric spaces to prepare for functional analysis, given your lack of exposure.
- **Study Time**: 8–10 weeks (extra time due to new topic)
- **Resources**:
  - _Principles of Mathematical Analysis_ by Walter Rudin (rigorous, standard)
  - _Real Analysis: Modern Techniques and Their Applications_ by Gerald B. Folland (comprehensive)
  - _Understanding Analysis_ by Stephen Abbott (accessible, beginner-friendly)
  - MIT OpenCourseWare: Real Analysis (18.100)
  - YouTube: The Bright Side of Mathematics
- **Practice**: Prove convergence of a numerical GNC algorithm, compute Lebesgue integrals for sensor data.

### 13. Functional Analysis

- **Subtopics** (based on your course description):
  - **Banach Spaces**: Norms, completeness, duality, Hahn-Banach theorems (separation, extension).
  - **Hilbert Spaces**: Inner products, orthogonality, orthonormal bases, Riesz representation theorem.
  - **C(K)-spaces and L^p-spaces**: Compact spaces, completeness, duality, approximation techniques.
  - **Bounded Linear Operators**: Uniform boundedness, open mapping, closed graph theorems, compact operators.
  - **Hilbert Space Operators**: Orthogonal projections, unitary/self-adjoint/normal operators, spectral theorem.
  - **Weak Derivatives and Sobolev Spaces**: Weak solutions, applications to PDEs, boundary value problems.
  - **Densely Defined Operators**: Closed operators, unbounded operators.
  - **Semigroups of Operators**: Generators, resolvent, Hille-Yosida theorem.
- **Relevance**:
  - Hilbert spaces underpin Kalman filters and optimal control for Mars navigation.
  - Sobolev spaces solve PDEs for asteroid gravitational models and Mars EDL.
  - Spectral theory analyzes stability in GNC systems for orbital refueling.
  - Semigroups model time-evolving spacecraft dynamics.
- **Streamlined Study**: Start with Hilbert spaces and Sobolev spaces, as they’re most relevant to GNC; defer advanced operator theory until comfortable with real analysis.
- **Study Time**: 8–10 weeks
- **Resources**:
  - _Introductory Functional Analysis with Applications_ by Erwin Kreyszig (accessible, applied)
  - _Functional Analysis_ by Walter Rudin (rigorous)
  - _Functional Analysis, Sobolev Spaces and Partial Differential Equations_ by Haim Brezis
  - _A Course in Functional Analysis_ by John B. Conway
  - MIT OpenCourseWare: Functional Analysis (18.102)
  - YouTube: Dr. Peyam’s Functional Analysis Playlist
- **Practice**: Design a Kalman filter using Hilbert spaces, solve a PDE for asteroid navigation.

### 14. Stochastic Processes

- **Subtopics**:
  - **Markov Processes**: Discrete/continuous time, transition probabilities, Chapman-Kolmogorov equations.
  - **Brownian Motion**: Properties, Wiener process, martingales.
  - **Stochastic Differential Equations**: Ito’s lemma, existence/uniqueness.
  - **Monte Carlo Methods**: Simulation, variance reduction, importance sampling.
  - **Filtering**: Kalman filter, extended Kalman filter, particle filters.
- **Relevance**:
  - Models uncertainties in sensor data and environmental conditions.
  - Kalman filters provide state estimation for Mars rovers and orbital navigation.
  - Monte Carlo methods assess risks in asteroid mining.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Introduction to Stochastic Processes_ by Gregory F. Lawler
  - _Stochastic Processes_ by Sheldon M. Ross
  - _Probability and Random Processes_ by Geoffrey Grimmett and David Stirzaker
  - MIT OpenCourseWare: Stochastic Processes (18.445)
  - YouTube: Mathematical Monk’s Stochastic Processes
- **Practice**: Implement an extended Kalman filter for a Mars rover, simulate asteroid uncertainties.

### 15. Advanced Control Theory

- **Subtopics**:
  - **State-Space Models**: Controllability, observability, state feedback, pole placement.
  - **Nonlinear Control**: Feedback linearization, sliding mode control, backstepping.
  - **Robust Control**: H-infinity control, mu-synthesis, uncertainty modeling.
  - **Optimal Control**: Linear quadratic regulator (LQR), linear quadratic Gaussian (LQG).
  - **Model Predictive Control (MPC)**: Receding horizon, constraints, real-time optimization.
- **Relevance**:
  - Nonlinear control handles complex spacecraft dynamics in deep space.
  - Robust control ensures stability in uncertain environments (e.g., crowded orbits).
  - MPC optimizes real-time GNC for asteroid mining robots.
- **Study Time**: 6–8 weeks
- **Resources**:
  - _Nonlinear Systems_ by Hassan K. Khalil
  - _Robust and Optimal Control_ by Kemin Zhou, John C. Doyle, and Keith Glover
  - _Model Predictive Control_ by James B. Rawlings, David Q. Mayne, and Moritz M. Diehl
  - Stanford Online: Control of Mobile Robots (free course)
  - YouTube: Steve Brunton’s Control Tutorials
- **Practice**: Design an LQR controller for orbital rendezvous, simulate MPC for a mining robot.

## Phase 5: Specialized Topics for Space Applications

Integrate mathematics with aerospace engineering for your goals.

### 16. Orbital Mechanics

- **Subtopics**:
  - **Two-Body Problem**: Kepler’s laws, orbital elements, vis-viva equation.
  - **Perturbations**: J2 effects, atmospheric drag, n-body interactions, secular perturbations.
  - **Low-Thrust Trajectories**: Electric propulsion, optimal transfers, Edelbaum’s equation.
  - **Interplanetary Transfers**: Hohmann transfers, patched conics, Lambert’s problem.
  - **Rendezvous and Docking**: Clohessy-Wiltshire equations, relative motion.
- **Relevance**:
  - Designs trajectories for Mars missions and orbital refueling.
  - Perturbation theory models asteroid orbits.
  - Lambert’s problem solves interplanetary navigation for mining missions.
- **Study Time**: 4–6 weeks
- **Resources**:
  - _Fundamentals of Astrodynamics_ by Roger R. Bate, Donald D. Mueller, and Jerry E. White
  - _Orbital Mechanics for Engineering Students_ by Howard D. Curtis
  - _Fundamentals of Astrodynamics and Applications_ by David A. Vallado
  - YouTube: Scott Manley’s Orbital Mechanics Videos
- **Practice**: Compute a Mars transfer orbit, simulate a rendezvous for refueling.

### 17. Attitude Dynamics and Control

- **Subtopics**:
  - **Rigid Body Dynamics**: Euler’s equations, moment of inertia tensor, angular momentum.
  - **Attitude Representations**: Euler angles, quaternions, rotation matrices, direction cosine matrices.
  - **Attitude Determination**: Star trackers, gyroscopes, sun sensors, magnetometers.
  - **Control Actuators**: Reaction wheels, control moment gyroscopes, thrusters.
  - **Stability**: Lyapunov-based attitude control, nutation damping.
- **Relevance**:
  - Ensures spacecraft orientation for navigation and communication.
  - Quaternions are standard in GNC for Mars landers and asteroid miners.
  - Stability analysis prevents tumbling in low-gravity environments.
- **Study Time**: 4–6 weeks
- **Resources**:
  - _Spacecraft Attitude Dynamics_ by Peter C. Hughes
  - _Spacecraft Dynamics and Control: An Introduction_ by Marcel J. Sidi
  - _Analytical Mechanics of Space Systems_ by Hanspeter Schaub and John L. Junkins
  - YouTube: Jorge Cremades’ Spacecraft Dynamics
- **Practice**: Simulate quaternion-based attitude control, design a reaction wheel system.

### 18. Estimation and Filtering

- **Subtopics**:
  - **Least Squares**: Linear/nonlinear least squares, weighted least squares, recursive least squares.
  - **Kalman Filtering**: Linear Kalman filter, extended Kalman filter (EKF), unscented Kalman filter (UKF).
  - **Particle Filters**: Sequential Monte Carlo, resampling, importance sampling.
  - **Sensor Fusion**: Combining IMU, GPS, vision, and radar data.
- **Relevance**:
  - Provides accurate navigation for Mars rovers and orbital spacecraft.
  - Particle filters handle nonlinear uncertainties in asteroid navigation.
  - Sensor fusion integrates data for robust GNC.
- **Study Time**: 4–6 weeks
- **Resources**:
  - _Optimal State Estimation_ by Dan Simon
  - _Estimation with Applications to Tracking and Navigation_ by Yaakov Bar-Shalom, X. Rong Li, and Thiagalingam Kirubarajan
  - _Kalman Filtering: Theory and Practice_ by Mohinder S. Grewal and Angus P. Andrews
  - YouTube: Michel van Biezen’s Kalman Filter Videos
- **Practice**: Implement an EKF for a Mars rover, simulate sensor fusion for orbital navigation.

## Study Order and Timeline

- **Months 1–3**: Calculus I (complete self-study), start Calculus II
- **Months 4–6**: Calculus II, start Linear Algebra
- **Months 7–9**: Linear Algebra, Calculus III
- **Months 10–12**: ODEs, start Probability and Statistics
- **Months 13–15**: Probability and Statistics, Numerical Analysis
- **Months 16–18**: PDEs, Classical Control Theory
- **Months 19–21**: Dynamical Systems, Optimization
- **Months 22–24**: Real Analysis, start Functional Analysis
- **Months 25–27**: Functional Analysis, Stochastic Processes
- **Months 28–30**: Advanced Control Theory, Orbital Mechanics
- **Months 31–33**: Attitude Dynamics, Estimation and Filtering

