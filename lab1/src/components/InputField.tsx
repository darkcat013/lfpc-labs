import { Box, Button, Grid, TextField } from '@mui/material';
import React, { useState, useEffect, useRef } from 'react';
import { finiteAutomation } from '../functions/FiniteAutomation';
import { GrammarTextFieldInfo } from '../interfaces';
import DynamicGraph from './DynamicGraph';
import StringChecker from './StringChecker';

const InputField: React.FC = () => {
	const [input, setInput] = useState(new Map<string, string[]>([
		["S", ["aS", "bS", "cA"]],
		["A", ["aB"]],
		["B", ["aB", "bB", "c"]]
	]));

	const [textFieldsValues, setTextFieldsValues] = useState<GrammarTextFieldInfo[]>([]);
	const defaultValues = useRef<GrammarTextFieldInfo[]>([]);
	useEffect(() => {
		defaultValues.current.push({ key: "S", value: "aS" });
		defaultValues.current.push({ key: "S", value: "bS" });
		defaultValues.current.push({ key: "S", value: "cA" });
		defaultValues.current.push({ key: "A", value: "aB" });
		defaultValues.current.push({ key: "B", value: "aB" });
		defaultValues.current.push({ key: "B", value: "bB" });
		defaultValues.current.push({ key: "B", value: "c" });
		setTextFieldsValues(defaultValues.current);
	}, [])

	const generateInput = () => {
		let map = new Map<string, string[]>();
		let keys = [...new Set<string>(textFieldsValues.map(x => x.key))];
		for (let key of keys) {
			let values = textFieldsValues.filter(k => k.key === key).map(x => x.value);
			map.set(key, values);
		}
		setInput(map);
	}

	const addTextField = () => {
		let tempValues = textFieldsValues.slice();
		tempValues.push({ key: "", value: "" });
		setTextFieldsValues(tempValues);
	}

	const deleteTextField = () => {
		let tempValues = textFieldsValues.slice();
		if (tempValues.length > 2) {
			tempValues.pop();
		}
		else alert("too few text fields");
		setTextFieldsValues(tempValues);
	}

	const handleKeyChange = (event: any, index: any) => {
		const tempValues = textFieldsValues.slice();
		tempValues[index].key = event.target.value;
		setTextFieldsValues(tempValues);
	}

	const handleValueChange = (event: any, index: any) => {
		const tempValues = textFieldsValues.slice();
		tempValues[index].value = event.target.value;
		setTextFieldsValues(tempValues);
	}
	let textFields = textFieldsValues.map((x: any, index: any) => {
		return (
			<Grid item key={index} sx={{ mt: 1 }}>
				<Grid container direction="row">
					<Grid item sx={{ width: 50 }}>
						<TextField defaultValue={x.key} size="small" inputProps={{ maxLength: 1 }} onChange={(e) => handleKeyChange(e, index)} />
					</Grid>
					<Grid item sx={{ fontSize: 24 }}>-&gt;</Grid>
					<Grid item sx={{ width: 60 }}>
						<TextField value={x.value} size="small" inputProps={{ maxLength: 2 }} onChange={(e) => handleValueChange(e, index)} />
					</Grid>
				</Grid>
			</Grid>
		)
	})

	return (
		<Grid container direction="row" sx={{ ml: 2, mt: 2 }}>
			<Grid item xs={3} container direction="column" >
				<Grid item>
					<Grid container direction="column">
						<Grid item container direction="column">
							{textFields}
						</Grid>
						<Grid item container direction="row" sx={{ mt: 2 }}>
							<Grid item>
								<Button color="primary" variant="contained" onClick={addTextField}>Add</Button>
							</Grid>
							<Grid item sx={{ ml: 2 }}>
								<Button color="primary" variant="outlined" onClick={generateInput}>Generate</Button>
							</Grid>
							<Grid item sx={{ ml: 2 }}>
								<Button color="secondary" variant="outlined" onClick={deleteTextField}>Delete</Button>
							</Grid>
						</Grid>
					</Grid>
				</Grid>
				<Grid item sx={{ mt: 3, pt: 3, borderTop: 1 }}>
					<StringChecker map={input} relations = {finiteAutomation(input)} />
				</Grid>
			</Grid>
			<Grid item xs={6} sx={{borderRight: 1}}>
				<DynamicGraph relations={finiteAutomation(input)} />
			</Grid>
			<Grid item xs={3} sx={{ pl: 1 }}>
				<Box >
					Variant 15. <br />
					VN=&#123;S, A, B&#125;, <br />
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
		</Grid>
	);
}

export default InputField;