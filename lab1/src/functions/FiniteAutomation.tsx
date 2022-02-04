import { Relation } from "../interfaces";

export const finiteAutomation = (input: Map<string, string[]>): Relation[] => {
	let output: Relation[] = [];
	let outputNodes: Map<string, string> = new Map<string, string>();

	let i = 0;
	for (let key of input.keys()) {
		outputNodes.set(key, "q" + i);
		i++;
	}

	for (let [key, values] of input) {
		for (let value of values) {
			if (value.match(/^[a-z]{1}[A-Z]{1}$/)) {
				output.push({
					from: outputNodes.get(key),
					to: outputNodes.get(value[1]),
					edgeLabel: value[0],
				})
			}
			else {
				output.push({
					from: outputNodes.get(key),
					to: "q"+input.size,
					edgeLabel: value[0],
				})
			}
		}
	}
	output.push({
		from: "q"+input.size
	})
	return output;
}

