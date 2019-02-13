# Unity Bits

This repository contains snippets and examples of helpful effects and scripts I've used.

## Sweep Reveal

Simple effect to transition between alpha=0 and alpha=1 with a sweeping blend.  Good for revealing panels or terrain layers, for example.

Shader can be found at `UnityHelpers/Assets/Sweep/SweepReveal.shader`

See `UnityHelpers/Assets/Sweep/Example/Sweep Sample Scene` for an example.

## Occlusion only

Shader that writes to the depth buffer, but does not render any colour.

Note the Render Queue is `Geometry-1` to ensure it renders first - or there isn't anything to occlude!

Shader can be found at `UnityHelpers/Assets/Occlusion/Occlusion Only.shader`

See `UnityHelpers/Assets/Occlusion/Example/Occlusion Scene` for an example.