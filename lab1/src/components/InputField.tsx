import { Box, Button, Grid } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { finiteAutomation } from '../functions/FiniteAutomation';
import { Relation } from '../interfaces';
import DynamicGraph from './DynamicGraph';
import StringChecker from './StringChecker';

const InputField: React.FC = () => {
	const [input, setInput] = useState(new Map<string, string[]>([
		["S", ["aS", "bS", "cA"]],
		["A", ["aB"]],
		["B", ["aB", "bB", "c"]]
	]));

	return (
		<Grid container direction="row" xs={12} sx={{ ml: 2, mt: 2 }}>
			<Grid item xs = {3} container  direction = "column" >
				<Grid item>
					<Box >
						Variant 15. <br/>
						VN=&#123;S, A, B&#125;, <br/>
						VT=&#123;a, b, c&#125;, <br />
						P=&#123;<br />
						1. S -&gt; aS<br />
						2. S -&gt; bS<br />
						3. S -&gt; cA<br />
						4. A -&gt; aB<br />
						5. B -&gt; aB<br />
						6. B -&gt; bB<br />
						7. B -&gt; c &#125;
					</Box>
				</Grid>
				<Grid item sx = {{mt:3}}>
					<StringChecker map = {input} />
				</Grid>
			</Grid>
			<Grid item xs = {9}>
				<DynamicGraph relations={finiteAutomation(input)} />
			</Grid>
		</Grid>
	);
}

export default InputField;