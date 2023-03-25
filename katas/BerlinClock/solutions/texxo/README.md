# BerlinClock solution supposed by Kai Bielefeld | texxo

## setup
- Angular
  - update angular cli `npm install -g @angular/cli@latest` to ensure to use latest angular version when initializing a new project
  - while installing angular via `ng new berlin-clock` the following config was selected:
    - `? Would you like to add Angular routing? Yes`
    - `? Which stylesheet format would you like to use? SCSS`
- Start the app:
  - `~/development/CodingDojo/katas/BerlinClock/solutions/texxo/berlin-clock && ng serve --open`

## implementation
- generate components
  - smart berlin-clock component to display a clock-context: `ng generate component views/components/berlin-clock`
  - dump time-interval-block component to configure and display a single block which can be used within a more complex context: `ng generate component views/components/time-interval-block`
  - no extra component for status LED (would have been possible - but an overkill)
- generate service
  - time-provider service for providing us the current time: `ng generate service core/services/time-provider`
- implement type for different block-types
  - defined `TimeIntervalBlockType` for explicitly declaring different block-types to differentiating between them (for styling reasons)
- explicitly waived async implementations and tests