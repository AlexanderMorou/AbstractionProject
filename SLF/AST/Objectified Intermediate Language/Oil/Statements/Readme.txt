 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

Notes on statements:
    * Switch statements
        To properly structure the jump tables associated to the switch statements, you'll need to
        build a simple structure to manage the values->switch case label.

        This can be done by iterating the cases, evaluating their values, and then ordering
        them by their numeric value.  After that, do a search through and start switch segments
        which break only when there's a 30-50 point gap between the value currently and the
        last known value.

        The 'gaps' will be filled by forwards to the label of the default case; however,
        if the default case is undefined, it should forward to the exit point of the switch.